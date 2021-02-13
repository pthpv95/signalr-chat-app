<template>
  <b-container>
    <b-card>
      <b-form @submit="onSubmit" @reset="onReset" v-if="show">
        <b-form-group
          id="input-group-1"
          label="User name"
          label-for="input-1"
        >
          <b-form-input
            id="input-1"
            v-model="form.userName"
            required
            autocomplete="off"
            placeholder="Enter user name"
          ></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-2" label="Password">
          <b-form-input type="password" v-model="form.password" required placeholder="Password" autocomplete="off"></b-form-input>
          <b-form-invalid-feedback :state="validation">Your user ID must be 5-12 characters long.</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group id="input-group-3" label="First name:" label-for="input-2">
          <b-form-input id="input-3" v-model="form.firstName" required placeholder="First name" autocomplete="off"></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-4" label="Last Name:" label-for="input-2">
          <b-form-input id="input-4" v-model="form.lastName" required placeholder="Last name" autocomplete="off"></b-form-input>
        </b-form-group>
        <b-button type="submit" variant="primary">Submit</b-button>
      </b-form>
    </b-card>
  </b-container>
</template>

<script>
import { registerUser } from './services';

export default {
  data() {
    return {
      form: {
        email: "",
        firstName: "",
        lastName: "",
        password: "",
        userName: "",
        food: null,
        checked: []
      },
      foods: [
        { text: "Select One", value: null },
        "Carrots",
        "Beans",
        "Tomatoes",
        "Corn"
      ],
      show: true
    };
  },
  methods: {
    async onSubmit(evt) {
      evt.preventDefault();
      const result = await registerUser(this.form.firstName, this.form.lastName, this.form.userName, this.form.password);
      const data = result.data;
      localStorage.setItem('user_info', JSON.stringify({
        firstName: data.firstName,
        lastName: data.lastName,
        userName: data.lastName,
        id: data.id
      }));
      this.$router.push('/');
    },
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
      this.form.email = "";
      this.form.name = "";
      this.form.food = null;
      this.form.checked = [];
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    }
  },
  computed: {
    validation(e) {
      if(this.form.password === ''){
        return true;
      }
      return this.form.password.length > 5;
    }
  }
};
</script>