<template>
  <b-card>
    <b-form @submit.stop.prevent @submit="onFormSubmitted">
      <b-form-group id="input-group-1" label="User name" label-for="input-1">
        <b-form-input
          id="input-1"
          v-model="username"
          required
          autocomplete="off"
          placeholder="Enter user name"
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-2" label="Password">
        <b-form-input
          type="password"
          v-model="password"
          required
          placeholder="Password"
          autocomplete="off"
        ></b-form-input>
        <b-form-invalid-feedback :state="validation">Your user ID must be 5-12 characters long.</b-form-invalid-feedback>
      </b-form-group>
      <b-button @click="onFormSubmitted">Submit</b-button>
    </b-form>
  </b-card>
</template>
<script>

export default {
  data() {
    return {
      username: "",
      password: ""
    };
  },
  methods: {
    onFormSubmitted() {
      console.log(this.username, this.password);
    }
  },
  computed: {
    validation() {
      return true;
    }
  }
};
</script>