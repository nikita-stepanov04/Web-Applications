<template>
  <blue-gradient-layout>
    <dismissible-alert ref="dismissibleAlert"></dismissible-alert>
    <form action="#"
          @submit.prevent="submitForm"
          ref="regForm"
          class="needs-validation"
          id="register-form"
          novalidate>
      <h2 class="text-center mb-3">Log in</h2>
      <div class="row">
        <div class="col-12">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input class="form-control"
                   v-model="loginData.email"
                   type="email"
                   id="email"
                   placeholder="Enter email"
                   required>
            <div class="invalid-feedback">
              Please enter email
            </div>
          </div>
        </div>
        <div class="col-12">
          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input class="form-control"
                   v-model="loginData.password"
                   id="password"
                   type="password"
                   placeholder="Enter password"
                   required
            >
            <div class="invalid-feedback">
              Please enter password
            </div>
          </div>
        </div>
      </div>
      <div class="d-grid my-4">
        <button class="btn btn-primary" type="submit">Log in</button>
        <router-link
            to="/register"
            class="link-secondary text-center mt-2"
        >
          Register
        </router-link>
      </div>
    </form>
  </blue-gradient-layout>
</template>

<script>
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor"
import BlueGradientLayout from "@/components/blueGradientLayout/BlueGradientLayout.vue";
import DismissibleAlert from "@/components/UI/DismissibleAlert.vue";

export default {
  components: {BlueGradientLayout, DismissibleAlert},
  mixins: [helpers, request],

  data() {
    return {
      loginData: {
        email: '',
        password: '',
      }
    }
  },
  methods: {
    async submitForm() {
      if (this.validateForm()) {

        try {
          const response = await request.post('auth', this.loginData, true)
          const data = response.data;
          console.log(data);
          this.$store.dispatch('updateUser', data.role);
          this.$router.push(this.$route.query.redirect || '/')

        } catch (error) {
          this.$refs.dismissibleAlert.alert('alert-danger',
              error.response.status === 401
                  ? 'Invalid username or password'
                  : 'Something went wrong');
        }
      }
    }
  }
}
</script>