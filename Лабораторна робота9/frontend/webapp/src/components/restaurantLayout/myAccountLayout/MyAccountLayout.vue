<template>
  <div class="main-block orders mt-4 mx-sm-5 mb-5">
    <form action="#"
          @submit.prevent="submitForm"
          ref="regForm"
          class="needs-validation"
          id="register-form"
          novalidate>
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-4 offset-md-4 col-8 offset-0">
            <h3 class="text-md-center text-start mt-2 mb-4">My Account</h3>
          </div>
          <div class="col-auto ms-auto">
            <a
                class="btn btn-outline-success"
                @click="logout">
              Log out
            </a>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <p class="form-control"
               id="email"
            >{{ user.email }}</p>
            <div class="invalid-feedback">
              Please enter email
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12x">
          <div class="mb-3">
            <label for="currentPassword" class="form-label">Enter current Password</label>
            <input class="form-control"
                   v-model="user.currentPassword"
                   id="currentPassword"
                   type="password"
                   placeholder="Enter current password"
                   pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,100}$"
            >
            <div class="invalid-feedback">
              Please enter password, must contain one lowercase,
              one uppercase character, number and one non-alphanumeric character
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-xs-12x">
          <div class="mb-3">
            <label for="newPassword" class="form-label">Enter new Password</label>
            <input class="form-control"
                   v-model="user.newPassword"
                   id="newPassword"
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
            <label for="houseNumber" class="form-label">House Number</label>
            <input class="form-control"
                   v-model="user.houseNumber"
                   type="number"
                   id="houseNumber"
                   placeholder="Enter houseNumber"
                   min="1"
            >
            <div class="invalid-feedback">
              Please enter house number
            </div>
          </div>
        </div>
      </div>
      <div class="d-flex my-4">
        <button class="btn btn-success flex-grow-1 me-1" type="submit">Save changes</button>
        <a class="btn btn-secondary flex-grow-1 ms-1"
           @click="discardChanges"
        >
          Discard changes
        </a>
      </div>
    </form>
  </div>
</template>

<script>
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor"

export default {
  mixins: [helpers, request],

  data() {
    return {
      user: {},
      userCopy: {},
    }
  },
  methods: {
    async submitForm() {
      if (this.validateForm()) {
        if (this.user.newPassword && !this.user.currentPassword) {
          this.alertDanger('Enter currant password')
        } else {
          if (JSON.stringify(this.user) !== JSON.stringify(this.userCopy)) {
            try {
              await request.put('auth/edit-user', this.user, true)
              this.userCopy = Object.assign({}, this.user);
              this.alertSuccess('Changes were applied')
            } catch (error) {
              this.alertDanger('Failed to apply changes');
            }
          } else {
            this.alertWarning('Nothing changed');
          }
        }
      }
    },
    async discardChanges() {
      if (JSON.stringify(this.user) !== JSON.stringify(this.userCopy)) {
        await this.getUserData();
        this.alertSuccess('Changes were discard')
      } else {
        this.alertWarning('Nothing changed');
      }
    },
    logout() {
      this.$store.dispatch('logout')
      this.$router.push('/menu');
    },
    async getUserData() {
      try {
        const data = (await request.get('auth/user-info', {}, true)).data;
        data.birthday = data.birthday.split('T')[0] // get rid of time part
        this.user = data;
        this.userCopy = Object.assign({}, data)
      } catch (error) {
        this.alertDanger('Something went wrong, can not load user data');
      }
    }
  },
  mounted() {
    this.addPhoneMask(this.$refs.phone)
    this.restrictDateInputWithYesterdayDate(this.$refs.birthday)
  },
  async created() {
    await this.getUserData();
  }
}
</script>