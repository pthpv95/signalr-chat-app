<template>
  <div class="header" v-show="!headerState">
    <b-navbar toggleable="lg" type="dark">
      <b-navbar-brand href="#">
        <router-link to="/">Home</router-link>
      </b-navbar-brand>
      <!-- <b-navbar-toggle target="nav-collapse"></b-navbar-toggle> -->
      <b-nav class="ml-auto">
        <!-- <b-nav-form>
          <b-form-input size="sm" class="mr-sm-2" placeholder="Search"></b-form-input>
          <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
        </b-nav-form>-->
        <b-navbar-nav>
          <b-nav-item href="#">
            <h5 class="number-of-noti" v-if="unreadMessages !== 0">{{unreadMessages}}</h5>
            <router-link to="/chat">
              <img class="inbox-icon" :src="inboxIcon" />
            </router-link>
          </b-nav-item>
        </b-navbar-nav>
        <b-nav-item-dropdown right no-caret>
          <!-- Using 'button-content' slot -->
          <template v-slot:button-content>
            <div v-if="!user.avatar" class="default-avatar">
              {{user.avatarAlias}}
            </div>
            <p>{{user.firstName}}</p>
          </template>
          <b-dropdown-item href="#">
            <router-link to="/profile">Profile</router-link>
          </b-dropdown-item>
          <b-dropdown-item @click="onSignOut">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-nav>
    </b-navbar>
    <hr>
  </div>
</template>

<script>
import notificationIcon from "../assets/images/notification-icon.svg";
import inboxIcon from "../assets/images/inbox-icon.png";
import AuthService from "../services/AuthService";
import * as signalr from "@microsoft/signalr";
import { BASE_URL, getAsync } from "../services/HttpClient";
import { mapGetters, mapActions } from 'vuex'

export default {
  data() {
    return {
      user: { avatarAlias: '', firstName: '' },
      numberOfNotifications: 0,
      unreadMessages: 0,
      notificationIcon,
      inboxIcon,
      authService: new AuthService(),
    };
  },
  async mounted() {
    const token = localStorage.getItem("access_token") || null;
    if (token) {
      getAsync(BASE_URL + "api/users").then((res) => {
        const data = res.data;
        localStorage.setItem('user_info', JSON.stringify(data));
        this.user.avatarAlias = data.abbreviatedName;
        this.user.firstName = data.firstName;
      });

      this.connection = new signalr.HubConnectionBuilder()
        .withUrl(`${BASE_URL}hub/notification`, {
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
        .withAutomaticReconnect(3)
        .configureLogging(signalr.LogLevel.Error)
        .build();

      await this.connection.start();

      this.connection.on("HasNewNotificationsAsync", (data) => {
        this.numberOfNotifications = data;
      });

      this.connection.on("HasUnreadMessagesAsync", (data) => {
        this.unreadMessages = data;
      });
    }
  },
  methods: {
    ...mapActions('ui', ['hideHeader']),
    onClickGoToProfile() {
      this.$router.push("/profile");
    },
    onSignOut() {
      localStorage.clear();
      this.authService.signOut();
    },
  },
  computed: {
    ...mapGetters('ui', ['headerState'])
  },
};
</script>

<style>
.header {
  background-color: gray;
}
.avatar {
  border-radius: 50%;
  width: 40px;
}
.inbox-icon {
  width: 40px;
}

.dropdown-toggle::after {
  content: none;
}
.default-avatar{
  border-radius: 50%;
  width: 40px;
  height: 40px;
  display: table-cell;
  text-align: center;
  vertical-align: middle;
  background-color: white;
}
.number-of-noti {
  position: absolute;
  right: 90px;
  top: 9px;
  background: rgb(204, 0, 0);
  border-radius: 50%;
  color: #ffffff;
  min-width: 25px;
}
</style>