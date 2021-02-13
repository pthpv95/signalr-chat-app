<template>
  <b-container>
    <div>
      <h2>Hello {{user.firstName}}</h2>
    </div>
    <div>
      <b-container class="chat-box">
        <b-row>
          <b-col cols="4" class="contacts">
            Chats
            <div
              :class="{'card-contact': selectedContact.id !== contact.id,
                      'card-contact selected': selectedContact.id === contact.id}"
              v-for="contact in contacts"
              :key="contact.id"
              @click="() => onContactClick(contact.id)"
              :id="contact.id"
            >{{contact.name}}</div>
          </b-col>
          <b-col cols="8">
            <h3>{{selectedContact.id !== '' ? selectedContact.name : ''}}</h3>
            <div class="chat-content">
              <div
                class="message-content align-right"
                v-for="message in messages"
                :key="message.id"
              >
                <p
                  :class="{'align-right': message.type === 'sender', 'align-left': message.type === 'recipient'}"
                >{{message.content}}</p>
              </div>
            </div>
            <form v-on:submit.prevent="onSubmit">
              <div class="send-message-box">
                <input
                  class="send_message_input"
                  v-model="message"
                  placeholder="Message here"
                />
                <button type="submit">Send</button>
              </div>
            </form>
          </b-col>
        </b-row>
      </b-container>
    </div>
  </b-container>
</template>

<script>
import jwtDecode from "jwt-decode";
import shortId from "shortid";
export default {
  data() {
    return {
      user: {
        firstName: "",
        lastName: ""
      },
      contacts: [],
      messages: [
        {
          id: shortId(),
          content: "message 1",
          type: "sender",
          sentAt: new Date()
        },
        {
          id: shortId(),
          content: "message 2",
          type: "sender",
          sentAt: new Date()
        },
        {
          id: shortId(),
          content: "message 3",
          type: "recipient",
          sentAt: new Date()
        },
        {
          id: shortId(),
          content: "message 4",
          type: "sender",
          sentAt: new Date()
        },
        {
          id: shortId(),
          content: "message 5",
          type: "recipient",
          sentAt: new Date()
        }
      ],
      selectedContact: {
        id: ""
      },
      message: ""
    };
  },
  async mounted() {
    const token = localStorage.getItem("access_token");
    const payload = jwtDecode(token);
    this.user = {
      firstName: payload.userName
    };

    this.contacts = [
      {
        id: shortId(),
        name: "contact 1"
      },
      {
        id: shortId(),
        name: "contact 2"
      },
      {
        id: shortId(),
        name: "contact 3"
      }
    ];
  },
  methods: {
    logout() {
      this.user = null;
      localStorage.removeItem("user_info");
      this.$router.push("/sign-up");
    },
    onContactClick(id) {
      this.selectedContact = this.contacts.find(c => c.id === id);
    },
    onSubmit() {
      this.messages.push({
        id: shortId(),
        content: this.message,
        type: "sender",
        sentAt: new Date()
      });
      this.message = "";
    }
  },
  computed: {}
};
</script>

<style lang="scss">
.contacts {
  // height: 600px;
}
.card-contact {
  text-align: left;
  height: 60px;
  padding: 15px;
  border: lightgray solid 1px;
  &.selected {
    background-color: lightgray;
  }
}

.chat-content {
  height: 600px;
  border-left: double;
  overflow-y: scroll;
}

.send-message-box {
    position: absolute;
    bottom: 0;
    width: 700px;
    .send_message_input {
      width: 700px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }
  }
  .message-content {
    .align-left {
      text-align: left;
    }
    .align-right {
      text-align: right;
    }
    padding: 10px;
    border: lightgray solid 1px;
    border-radius: 2px;
    // width: 350px;
  }

.chat-box {
  border-style: ridge;
}
</style>