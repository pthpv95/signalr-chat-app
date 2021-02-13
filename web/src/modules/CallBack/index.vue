<template>
  <beat-loader></beat-loader>
</template>

<script>
import AuthService from '../../services/AuthService';
import BeatLoader from '../../components/BeatLoader';

export default {
  data(){
    return {
      authService: new AuthService(),
    }
  },
  mounted(){
    this.authService.signinRedirectCallback().then((user) => {
      if(user){
        localStorage.setItem("access_token", user.access_token);
        window.location.href = '/';
      }else{
        this.authService.signOut();
      }
    })
  },
  components:{
    'beat-loader': BeatLoader
  },
  props: {
    signinParams: String
  }
}
</script>

<style>

</style>