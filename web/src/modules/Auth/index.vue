<template>
  <div style="width: 500px; margin: 0 auto">
    <beat-loader :size="30"></beat-loader>
  </div>
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
    this.authService.signIn();
  },
  components:{
    'beat-loader': BeatLoader
  }
}
</script>

<style>

</style>