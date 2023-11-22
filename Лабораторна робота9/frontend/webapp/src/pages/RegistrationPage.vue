<template>
  <blue-gradient-layout>
    <dismissible-alert ref="dismissibleAlert"></dismissible-alert>
    <form action="#"
          @submit.prevent="submitForm"
          ref="regForm"
          class="needs-validation"
          id="register-form"
          novalidate>
      <h2 class="text-center mb-3">Register</h2>
      <div class="row">
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input class="form-control"
                   v-model="registrant.email"
                   type="email"
                   id="email"
                   placeholder="Enter email"
                   required>
            <div class="invalid-feedback">
              Please enter email
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input class="form-control"
                   v-model="registrant.password"
                   id="password"
                   type="password"
                   placeholder="Enter password"
                   required
                   pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,100}$"
            >
            <div class="invalid-feedback">
              Please enter password, must contain one lowercase,
              one uppercase character, number and one non-alphanumeric character
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="name" class="form-label">Name</label>
            <input class="form-control"
                   v-model="registrant.name"
                   id="name"
                   placeholder="Enter name"
                   required
                   pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
            >
            <div class="invalid-feedback">
              Please enter your name
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="surname" class="form-label">Surname</label>
            <input class="form-control"
                   v-model="registrant.surname"
                   id="surname"
                   placeholder="Enter surname"
                   required
                   pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
            >
            <div class="invalid-feedback">
              Please enter surname
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="birthday" class="form-label">Birthday</label>
            <input class="form-control"
                   v-model="registrant.birthday"
                   id="birthday"
                   type="date"
                   ref="birthday"
                   required
            >
            <div class="invalid-feedback">
              Please enter your date of birth
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <label class="form-label">Choose your gender</label>
          <div class="mb-3">
            <div class="form-check">
              <input class="form-check-input"
                     v-model="registrant.gender"
                     type="radio"
                     name="gender"
                     id="male"
                     required
                     value="male"
              >
              <label class="form-check-label" for="male">Male</label>
            </div>
            <div class="form-check">
              <input class="form-check-input"
                     v-model="registrant.gender"
                     type="radio"
                     name="gender"
                     id="female"
                     value="female"
              >
              <label class="form-check-label" for="female">Female</label>
            </div>
            <div class="invalid-feedback">
              Please choose your gender
            </div>
          </div>
        </div>
        <div class="col-sm-12 col-xs-12">
          <div class="mb-3">
            <label class="form-label" for="phone">Enter your phone</label>
            <input class="form-control phone-input"
                   v-model="registrant.phone"
                   id="phone"
                   ref="phone"
                   type="tel"
                   required
                   pattern="^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$"
            >
            <div class="invalid-feedback" id="phone-validation">
              Please enter your phone number
            </div>
          </div>
        </div>
      </div>
      <div class="d-grid my-4">
        <button class="btn btn-primary" type="submit">Register</button>
        <router-link
            to="/login"
            class="link-secondary text-center mt-2"
        >
          Log in
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
      registrant: {
        email: '',
        name: '',
        surname: '',
        password: '',
        gender: '',
        birthday: '',
        phone: '',
      }
    }
  },
  mounted() {
    this.addPhoneMask(this.$refs.phone);
    this.restrictDateInputWithYesterdayDate(this.$refs.birthday);
  },
  methods: {
    async submitForm() {
      if (this.validateForm()) {
        try {
          await request.post('auth/register', this.registrant);
          this.$router.push('/login');
        } catch (error) {
          this.$refs.dismissibleAlert.alert('alert-danger',
              error.response.status === 409
                  ? 'Username is already taken'
                  : 'Something went wrong');
        }
      }
    }
  }
}
</script>