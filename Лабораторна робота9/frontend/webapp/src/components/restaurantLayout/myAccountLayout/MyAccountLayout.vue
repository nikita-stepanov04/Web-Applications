<template>
  <div class="main-block orders mt-4 mx-5 mb-5">
    <dismissible-alert ref="dismissibleAlert"></dismissible-alert>
    <form action="#"
          @submit.prevent="submitForm"
          ref="regForm"
          class="needs-validation"
          id="register-form"
          novalidate>
      <div class="container-fluid">
        <div class="row">
          <div class="col-4 offset-4">
            <h3 class="text-center mt-2 mb-4">My Account</h3>
          </div>
          <div class="col-auto ms-auto">
            <button
                class="btn btn-outline-success"
                @click="logout">
              Log out
            </button>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="email" class="form-label">Username</label>
            <input class="form-control"
                   type="email"
                   v-model="user.email"
                   id="email"
                   placeholder="Enter email"
            >
            <div class="invalid-feedback">
              Please enter email
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12x">
          <div class="mb-3">
            <label for="password" class="form-label">New Password</label>
            <input class="form-control"
                   v-model="user.password"
                   id="password"
                   type="password"
                   placeholder="Enter new password"
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
                   v-model="user.name"
                   id="name"
                   placeholder="Enter name"
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
                   v-model="user.surname"
                   id="surname"
                   placeholder="Enter surname"
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
                   v-model="user.birthday"
                   id="birthday"
                   type="date"
                   ref="birthday"
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
                     v-model="user.gender"
                     type="radio"
                     name="gender"
                     id="male"
                     value="male"
              >
              <label class="form-check-label" for="male">Male</label>
            </div>
            <div class="form-check">
              <input class="form-check-input"
                     v-model="user.gender"
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
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label class="form-label" for="phone">Enter your phone</label>
            <input class="form-control phone-input"
                   v-model="user.phoneNumber"
                   id="phone"
                   ref="phone"
                   type="tel"
                   pattern="^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$"
            >
            <div class="invalid-feedback" id="phone-validation">
              Please enter your phone number
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="city" class="form-label">City</label>
            <input class="form-control"
                   v-model="user.city"
                   id="city"
                   placeholder="Enter city"
                   pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
            >
            <div class="invalid-feedback">
              Please enter city
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="street" class="form-label">Street</label>
            <input class="form-control"
                   v-model="user.street"
                   id="street"
                   placeholder="Enter street"
                   pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
            >
            <div class="invalid-feedback">
              Please enter street
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12">
          <div class="mb-3">
            <label for="flat" class="form-label">Flat</label>
            <input class="form-control"
                   v-model="user.flat"
                   type="number"
                   id="flat"
                   placeholder="Enter flat"
                   min="1"
            >
            <div class="invalid-feedback">
              Please enter flat
            </div>
          </div>
        </div>
      </div>
      <div class="d-flex my-4">
        <button class="btn btn-success flex-grow-1 me-1" type="submit">Save changes</button>
        <button class="btn btn-secondary flex-grow-1 ms-1">Discard changes</button>
      </div>
    </form>
  </div>
</template>

<script>
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor"
import DismissibleAlert from "@/components/UI/DismissibleAlert.vue";

export default {
  mixins: [helpers, request],
  components: {DismissibleAlert},

  data() {
    return {
      user: {}
    }
  },
  methods: {
    submitForm() {
      if (this.validateForm()) {
        console.log('valid');
      }
    },
    logout() {
      this.$store.dispatch('logout')
      this.$router.push('/menu');
    }
  },
  mounted() {
    this.addPhoneMask(this.$refs.phone)
    this.restrictDateInputWithYesterdayDate(this.$refs.birthday)
  },
  async created() {
    try {
      const response = await request.get('auth/user-info', {}, true);
      const data = response.data;
      data.birthday = data.birthday.split('T')[0] // get rid of time part
      this.user = response.data;
    } catch (error) {
      this.$refs.dismissibleAlert.alert('alert-danger',
          'Something went wrong, can not load user data');
    }
  }
}
</script>