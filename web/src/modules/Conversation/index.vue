<template>
  <div class="container-fluid h-100 main_content">
    <div class="row justify-content-center h-100">
      <div class="col-md-4 col-xl-3 chat" v-if="isShowFriendList">
        <div class="card mb-sm-3 mb-md-0 contacts_card">
          <div class="card-header">
            <div class="input-group">
              <input type="text" placeholder="Search..." name class="form-control search" />
              <div class="input-group-prepend">
                <span class="input-group-text search_btn">
                  <i class="fas fa-search"></i>
                </span>
              </div>
            </div>
          </div>
          <div class="card-body contacts_body">
            <ul class="contacts">
              <Contact
                :key="contact.key"
                v-for="contact in contacts"
                :isSelected="selectedContact.id === contact.id"
                :typingConversations="typingConversations"
                :contact="contact"
                :onClick="onContactClicked"
              />
            </ul>
          </div>
          <div class="card-footer"></div>
        </div>
      </div>
      <div class="col-md-8 col-xl-6 chat" v-if="isShowChatBox">
        <div class="card">
          <div class="card-header msg_head">
            <div class="d-flex bd-highlight" v-if="selectedContact.id !== ''">
              <i class="fas fa-arrow-left back_botton" v-if="isMobileScreen" @click="onClickBack"></i>
              <div class="user_info">
                <span>{{selectedContact.name}}</span>
                <p>1767 Messages</p>
              </div>
              <div class="video_cam">
                <span>
                  <i class="fas fa-video"></i>
                </span>
                <span>
                  <i class="fas fa-phone"></i>
                </span>
              </div>
            </div>
            <span id="action_menu_btn">
              <i class="fas fa-ellipsis-v"></i>
            </span>
            <div class="action_menu">
              <ul>
                <li>
                  <i class="fas fa-user-circle"></i> View profile
                </li>
                <li>
                  <i class="fas fa-users"></i> Add to close friends
                </li>
                <li>
                  <i class="fas fa-plus"></i> Add to group
                </li>
                <li>
                  <i class="fas fa-ban"></i> Block
                </li>
              </ul>
            </div>
          </div>
          <BeatLoader :loading="isLoading"></BeatLoader>
          <div
            class="card-body msg_card_body"
            ref="cardBodyRef"
            v-if="selectedContact.id !== ''"
            @scroll="onScroll"
          >
            <Message v-for="message in messages" :key="message.id" :message="message" />
          </div>
          <p v-show="isTyping" class="typing-message">{{this.selectedContact.name}} is typing ... </p>
          <div v-if="selectedContact.id === '' && this.user">Hello {{this.user.userName}}</div>
          <form v-on:submit.prevent="onSubmit" v-if="selectedContact.id !== ''">
            <div class="card-footer">
              <div v-if="selectedFile">
                <img :src="selectedFile.previewSource" width="120px" height="120px" />
                <i class="fas fa-trash-alt" @click="onRemoveFile"></i>
              </div>
              <div class="input-group">
                <div class="input-group-append">
                  <span class="input-group-text attach_btn" @click="onUploadFile">
                    <i class="fas fa-image"></i>
                    <input
                      type="file"
                      accept="image/*"
                      hidden
                      ref="fileUploadRef"
                      @change="onFileChange"
                    />
                  </span>
                </div>
                <input
                  class="form-control type_msg"
                  v-model="newMessage"
                  placeholder="Type your message..."
                  ref="messageInputRef"
                  @keyup="onTyping"
                />
                <div class="input-group-append">
                  <span class="input-group-text send_btn" @click="onSubmit">
                    <i class="fas fa-location-arrow"></i>
                  </span>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import * as signalr from "@microsoft/signalr";
import defaultVueLogo from "../../assets/logo.png";
import Message from "./Message";
import Contact from "./Contact";
import BeatLoader from "../../components/BeatLoader";
import typingIcon from "../../assets/images/typing-icon.gif";
import { getContacts, getConversationInfo } from "./services";
import { uploadFile, BASE_URL } from "../../services/HttpClient";
import AuthService from "../../services/AuthService";
import shortId from "shortid";
import { mapActions } from 'vuex';

export default {
  name: "ChatBox",
  data() {
    return {
      connection: null,
      newMessage: "",
      hubConnection: null,
      defaultAvatar: defaultVueLogo,
      contacts: [],
      messages: [],
      selectedContact: {
        id: "",
      },
      message: "",
      authSevice: new AuthService(),
      user: null,
      selectedFile: null,
      selectedConversationId: null,
      typingConversations: [],
      failed: 0,
      cursor: null,
      loadMore: false,
      isLoading: false,
      isTyping: false,
      typingIcon,
      isMobileScreen: /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent),
      isShowFriendList: true,
      isShowChatBox: true
    };
  },
  components: {
    Message,
    Contact,
    BeatLoader,
  },
  props: {
    msg: String,
  },
  // life cycles
  destroyed() {
    this.selectedConversationId = null;
  },
  async mounted() {
    if(this.isMobileScreen){
      this.isShowChatBox = false;
    }
    const user = await this.authSevice.getProfile();
    this.user = {
      id: user.chat_user_id,
      userName: user.user_name,
    };

    const connection = new signalr.HubConnectionBuilder()
      .withUrl(`${BASE_URL}hub/chat`, {
        accessTokenFactory: async () => {
          return new AuthService().getSignIn().then((res) => {
            if (res) {
              return localStorage.getItem("access_token");
            }
          });
        },
        skipNegotiation: true,
        transport: 1,
      })
      .configureLogging(4)
      .withAutomaticReconnect([0, 3000, 5000, 10000, 15000, 30000])
      .build();

    this.connection = connection;
    async function start() {
      try {
        await connection.start();
      } catch (err) {
        console.log("error");
        this.authSevice.signIn();
        setTimeout(() => start(), 3000);
      }
    }

    this.showLoading();

    await start();

    connection.onreconnecting((error) => {
      console.log(`Connection lost due to error "${error}". Reconnecting.`);
    });

    connection.onreconnected(() => {
      this.authSevice.getProfile();
      console.log('Connection reestablished. Connected.');
    });

    const res = await getContacts();
    this.contacts = res.data.map((c) => ({ ...c, key: shortId.generate() }));
    this.hideLoading();

    this.connection.on("HasNewPrivateMessageAsync", (res) => {
      if (res.conversationId === this.selectedConversationId) {
        const isResponse = res.senderId !== this.user.id;
        this.loadMore = false;
        this.messages.push({
          id: res.messageId,
          sentBy: res.sentBy,
          text: res.message,
          messageType: res.messageType,
          attachmentUrl: res.attachmentUrl,
          isResponse,
          seen: res.seen,
          sentAt: res.sentAt,
        });
        if (isResponse) {
          const receiverId = res.senderId;
          console.log("read", this.selectedConversationId);
          this.connection.invoke("ReadMessage", res.messageId, receiverId);
          this.typingConversations = this.typingConversations.filter(
            (x) => x !== this.selectedConversationId
          );
          this.connection.invoke(
            "MessageStopTyping",
            this.selectedConversationId,
            this.selectedContact.userId
          );
        }
      } else {
        const contact = this.contacts.find((c) => c.sentBy === res.sentBy);
        console.log("new message in", contact.firstName);
      }
    });

    this.connection.on("ReceiveReadMessageAsync", (data) => {
      const lastMessage = this.messages.find((m) => m.id === data.messageId);
      if (
        lastMessage &&
        lastMessage.id === data.messageId &&
        data.seenerId !== this.user.id
      ) {
        lastMessage.seen = true;
        this.messages.forEach((mes, index) => {
          if (index === this.messages.length - 1) {
            mes.seen = true;
          } else {
            mes.seen = false;
          }
        });
      }
    });

    this.connection.on("Typing", (data) => {
      if(data.conversationId === this.selectedConversationId){
        this.isTyping = true;
        return;
      }

      this.typingConversations.push(data.conversationId);
      this.contacts = this.contacts.map((c) => {
        const typing = c.userId === data.contactUserId;
        if (typing) {
          return {
            ...c,
            typing,
            key: shortId.generate(),
          };
        }
        return c;
      });
    });

    this.connection.on("StopTyping", (data) => {
      if(data.conversationId === this.selectedConversationId && !this.newMessage){
        this.isTyping = false;
        return;
      }
      this.contacts = this.contacts.map((c) => {
        const isTyping = c.userId === data.contactUserId;
        if (isTyping) {
          return {
            ...c,
            typing: false,
            key: shortId.generate(),
          };
        }
        return c;
      });

      this.typingConversations = this.typingConversations.filter(
        (t) => t !== data.conversationId
      );
    });
  },
  updated() {
    if (this.$refs.cardBodyRef && !this.loadMore) {
      this.$refs.messageInputRef.focus();
      this.$refs.cardBodyRef.scrollTop =
        this.$refs.cardBodyRef.scrollHeight -
        this.$refs.cardBodyRef.clientHeight;
    }
  },
  methods: {
    ...mapActions('ui', ['hideHeader', 'showHeader', 'showLoading', 'hideLoading']),
    onContactClicked(id) {
      if (this.selectedContact.contactId === id) return;
      this.showLoading()
      const contact = this.contacts.find((c) => c.id === id);
      this.isTyping = contact.typing;

      this.selectedContact.id = contact.userId;
      const input = {
        contactUserId: contact.userId,
        cursor: null,
        conversationId: null
      };
      getConversationInfo(input).then((res) => {
        if (res) {
          const data = res.data;
          const selectedContact = this.contacts.find((c) => c.id === id);
          this.selectedContact = {
            ...selectedContact,
            title: data.conversation.title,
            conversationId: data.id,
            contactId: selectedContact.id,
            name: selectedContact.firstName + ' ' + selectedContact.lastName
          };
          this.selectedConversationId = data.conversation.id;
          this.cursor = data.nextCursor;
          this.messages = data.conversation.messages;
          this.newMessage = "";
          this.hideLoading()
          if(!this.isMobileScreen){
            this.$refs.messageInputRef.focus();
            this.$refs.cardBodyRef.scrollTop = this.$refs.cardBodyRef.scrollHeight;
          }else{
            this.isShowChatBox = true;
            this.isShowFriendList = false;
            this.hideHeader();
          }
          if (
            data.conversation.messages.length > 0 &&
            data.conversation.messages.some((m) => !m.seen)
          ) {
            const message =
              data.conversation.messages[data.conversation.messages.length - 1];
            if (message.isResponse) {
              const messageId = message.id;
              this.connection.invoke("ReadMessage", messageId, contact.userId);
            }
          }

          this.contacts.forEach((contact) => {
            contact.key = shortId.generate();
            contact.typing = false;
          })
        }
      });
    },
    onScroll() {
      if (this.$refs.cardBodyRef.scrollTop === 0 && this.cursor) {
        this.isLoading = true;
        this.loadMore = true;
        const input = {
          contactUserId: this.selectedContact.id,
          cursor: this.cursor,
          conversationId: this.selectedConversationId,
        };
        getConversationInfo(input).then((res) => {
          if (res) {
            const data = res.data;
            this.selectedConversationId = data.conversation.id;
            this.cursor = data.nextCursor;
            this.messages = [...data.conversation.messages, ...this.messages];
            this.isLoading = false;
            this.$refs.cardBodyRef.scrollTop = 5;
          }
        });
      }
    },
    onSubmit() {
      if (!this.newMessage.trim() && !this.selectedFile) {
        return;
      }

      if (this.selectedFile) {
        uploadFile(this.selectedFile.file)
          .then((res) => {
            const id = res.data;
            const fileURL = `${BASE_URL}files/${id}`;
            this.connection
              .invoke(
                "SendMessage",
                this.newMessage,
                fileURL,
                this.selectedContact.userId,
                this.selectedConversationId
              )
              .then(() => {
                this.newMessage = "";
                this.selectedFile = null;
              });
            return;
          })
          .catch((error) => {
            throw error;
          });
      } else {
        this.connection
          .invoke(
            "SendMessage",
            this.newMessage,
            null,
            this.selectedContact.userId,
            this.selectedConversationId
          )
          .then(() => {
            this.newMessage = "";
          });
      }

      this.connection.invoke(
        "MessageStopTyping",
        this.selectedConversationId,
        this.selectedContact.userId
      );
    },
    onUploadFile() {
      this.$refs.fileUploadRef.click();
    },
    onFileChange(e) {
      const files = e.target.files;
      this.selectedFile = {
        file: files[0],
      };
      const reader = new FileReader();
      reader.onload = () => {
        this.selectedFile = {
          ...this.selectedFile,
          previewSource: reader.result,
        };
      };
      if (files) {
        reader.readAsDataURL(files[0]);
      }
    },
    onRemoveFile() {
      this.selectedFile = null;
    },
    onTyping(e) {
      if (
        e.target.value &&
        this.typingConversations.every((x) => x !== this.selectedConversationId)
      ) {
        this.typingConversations.push(this.selectedConversationId);
        this.connection.invoke(
          "MessageTyping",
          this.selectedConversationId,
          this.selectedContact.userId
        );
      } else if (!e.target.value) {
        this.typingConversations = this.typingConversations.filter(
          (x) => x !== this.selectedConversationId
        );
        this.connection.invoke(
          "MessageStopTyping",
          this.selectedConversationId,
          this.selectedContact.userId
        );
      }
    },
    onClickBack(){
      this.selectedConversationId = null;
      this.selectedContact = {
        id: ''
      };
      this.isShowFriendList = true;
      this.isShowChatBox = false;
      this.showHeader();
    }
  },
  computed: {},
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.main_content {
  height: 100%;
  margin: 0;
  background: #7f7fd5;
  background: -webkit-linear-gradient(to right, #91eae4, #86a8e7, #7f7fd5);
  background: linear-gradient(to right, #91eae4, #86a8e7, #7f7fd5);
}

.typing-message{
  text-align: left;
  margin-left: 33px;
  bottom: 0;
  color: whitesmoke;
}
</style>
