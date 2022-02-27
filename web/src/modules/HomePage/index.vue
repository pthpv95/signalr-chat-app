<template>
  <div>
    <b-container>
      <b-row>
        <b-col cols="2">
          <div>
            <div v-for="contact in suggestedContacts" :key="contact.id">
            <p>suggested friends</p>
              <div>
                <h3>{{contact.firstName + ' ' + contact.lastName}}</h3>
                <button class="primary" @click="() => onFriendRequestedToAdd(contact.id)">Add friend</button>
              </div>
            </div>
          </div>
          <div>
            <div v-for="contact in contactsRequests" :key="contact.id">
            <p>friends requests</p>
              <h3>{{contact.firstName + ' ' + contact.lastName}}</h3>
              <a
                href="#"
                variant="primary"
                @click="() => onFriendRequestedApproved(contact.id)"
              >Approve</a>
            </div>
          </div>
        </b-col>
        <b-col cols="10">
          <div></div>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
import {
  getFriendSuggestions,
  getContactRequests,
  addContact,
  approveContactRequest,
} from "./services";
import AuthService from "../../services/AuthService";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Home",
  data() {
    return {
      isLoggedIn: false,
      user: null,
      suggestedContacts: [],
      contactsRequests: [],
      authSevice: new AuthService(),
    };
  },
  components: {},
  props: {
    msg: String,
  },
  computed: {
    ...mapGetters("ui", {
      loadingState: "loadingState",
    }),
  },
  created() {},
  async mounted() {
    this.showLoading();
    this.user = await this.authSevice.getProfile();
    if (this.user) {
      const contactsRequests = await getContactRequests();
      const friendSuggestions = await getFriendSuggestions();
      this.suggestedContacts = friendSuggestions.data;
      this.contactsRequests = contactsRequests.data;
      this.hideLoading();
    }
  },
  destroyed() {},
  methods: {
    ...mapActions("ui", ["showLoading", "hideLoading"]),
    onFriendRequestedToAdd(contactId) {
      addContact(contactId).then(async (res) => {
        if (res) {
          this.suggestedContacts = this.suggestedContacts.filter(
            (c) => c.id !== contactId
          );
        }
      });
    },
    onFriendRequestedApproved(id) {
      approveContactRequest(id).then((res) => {
        if (res) {
          this.contactsRequests = this.contactsRequests.filter(
            (c) => c.id !== id
          );
        }
      });
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
