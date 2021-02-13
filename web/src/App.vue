<template>
  <component :is="dynamicComponent" :[propsKey]="currentProps"></component>
</template>

<script>
import AuthService from "./services/AuthService";
import Layout from "./layout";
import CallBackPage from "./modules/CallBack";

export default {
  name: "app",
  data() {
    return {
      authService: new AuthService(),
      isAuthenticated: false,
      dynamicComponent: "Layout",
      currentProps: "",
      propsKey: "",
    };
  },
  components: {
    Layout,
    CallBack: CallBackPage,
  },
  async created() {},
  async mounted() {
    const url = this.$router.history.current.fullPath.substring(0, 9);
    if (url === "/callback") {
      const rest = this.$router.history.current.hash.substring(10);
      this.currentProps = `${url}#${rest}`;
      this.propsKey = "signinParams";
      this.dynamicComponent = "CallBack";
    }
    // const userInfo = localStorage.getItem('user_info');
    // getAsync(BASE_URL + "api/users").then((res) => {
    //   const data = res.data;
    //   localStorage.setItem('user_info', JSON.stringify(data))
    // });
  },
};
</script>

