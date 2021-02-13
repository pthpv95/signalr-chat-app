<template>
  <div id="app" :class="loadingState ? 'loading' : ''">
    <img :src="loadingIcon" class="loading" v-if="loadingState" />
    <Header></Header>
    <div class="main-page">
      <div id="messagesList"></div>
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Header from "./Header";
import loadingIcon from "../assets/images/loading-icon.gif";
import { mapGetters } from "vuex";

export default {
  data() {
    return {
      loadingIcon,
    };
  },
  computed: {
    ...mapGetters("ui", {
      loadingState: "loadingState",
    }),
  },
  components: {
    Header,
  },
};
</script>

<style lang='scss'>
.loading {
  opacity: 0.5;
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  .loading {
    width: 80px;
    position: absolute;
    margin: auto;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
  }
}
</style>