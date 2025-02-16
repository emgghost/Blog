<!-- pages/auth/login.vue -->
<template>
  <v-container>
    <v-form @submit.prevent="login">
      <v-text-field v-model="email" label="ایمیل"></v-text-field>
      <v-text-field v-model="password" label="رمز عبور" type="password"></v-text-field>
      <v-btn type="submit" color="primary">ورود</v-btn>
    </v-form>
  </v-container>
</template>

<script>
import {useApi} from "../../useApi";

export default {
  data() {
    return {
      email: '',
      password: ''
    };
  },
  methods: {
    async login() {
      try {
        const response = await useApi().login({
          email: this.email,
          password: this.password
        });
        localStorage.setItem('token', response.token);
        this.$router.push('/admin');
      } catch (error) {
        alert('ورود ناموفق!');
      }
    }
  }
};
</script>
