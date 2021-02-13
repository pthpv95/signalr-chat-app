<script>
import AuthService from '../../services/AuthService'
export default {
  data(){
    return {
      authService: new AuthService()
    }
  },
  mounted(){
    this.authService.signinSilent();
  }
}
</script>

<style>

</style>