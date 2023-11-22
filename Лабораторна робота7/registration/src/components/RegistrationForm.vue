<template>
  <div class="main-block">
    <form action="#" @submit.prevent="submitForm()" ref="regForm" class="needs-validation" id="register-form" novalidate>
      <h2 class="text-center mb-3">Register</h2>
      <div class="row">
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input class="form-control" v-model="registrant.email" type="email" id="email" placeholder="Enter email" required>
            <div class="invalid-feedback">
              Please enter email
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input class="form-control" v-model="registrant.password" id="password" type="password" placeholder="Enter password" required pattern=".{4,}">
            <div class="invalid-feedback">
              Please enter password (min 4 characters)
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label for="name" class="form-label">Name</label>
            <input class="form-control" v-model="registrant.name" id="name" placeholder="Enter name" required pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+">
            <div class="invalid-feedback">
              Please enter your name
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label for="surname" class="form-label">Surname</label>
            <input class="form-control" v-model="registrant.surname" id="surname" placeholder="Enter surname" required pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+">
            <div class="invalid-feedback">
              Please enter surname
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label for="birthday" class="form-label">Birthday</label>
            <input class="form-control" v-model="registrant.birthday" id="birthday" type="date" ref="maxDate" required>
            <div class="invalid-feedback">
              Please enter your date of birth
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label for="group" class="form-label">Group</label>
            <select class="form-select" v-model="registrant.group" id="group" aria-label="Choose group">
              <option value="IA-21">IA-21</option>
              <option value="IA-22">IA-22</option>
              <option selected value="IA-23">IA-23</option>
              <option value="IA-24">IA-24</option>
            </select>
            <div class="invalid-feedback">
              Please select a group
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <div class="mb-3">
            <label class="form-label" for="phone">Enter your phone</label>
            <input class="form-control phone-input" v-model="registrant.phone" id="phone" ref="phone" type="tel" required pattern="^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$">
            <div class="invalid-feedback" id="phone-validation">
              Please enter your phone number
            </div>
          </div>
        </div>
        <div class="col-lg-6 col-sm-12">
          <label class="form-label">Choose your gender</label>
          <div class="mb-3">
            <div class="form-check">
              <input class="form-check-input" v-model="registrant.gender" type="radio" name="gender" id="male" required value="male">
              <label class="form-check-label" for="male">Male</label>
            </div>
            <div class="form-check">
              <input class="form-check-input" v-model="registrant.gender" type="radio" name="gender" id="female" value="female">
              <label class="form-check-label" for="female">Female</label>
            </div>
            <div class="invalid-feedback">
              Please choose your gender
            </div>
          </div>
        </div>
        <div class="col-12">
          <div class="mb-3">
            <label for="avatar" class="form-label">Avatar</label>
            <input type="file" ref="avatar" @change="getFile" accept="image/*" class="form-control" id="avatar" placeholder="Upload your avatar">
          </div>
        </div>
      </div>
      <div class="d-grid my-4">
        <button class="btn btn-primary" type="submit">Register</button>
        <a href="#" class="link-secondary text-center mt-2">Log in</a>
      </div>
    </form>
  </div>
</template>

<script>
import Inputmask from 'inputmask'

export default {
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
        group: 'IA-23',
        avatar: ''
      }
    }
  },
  methods: {
    submitForm() {
      const form = this.$refs.regForm;
      if (form.checkValidity()) {

        const newReg = Object.assign({}, this.registrant);
        newReg.id = this.$store.state.newId;
        this.$store.commit('incrementId');
        this.$store.commit('addRegistrant', newReg);

        form.reset();
        this.registrant.group = 'IA-23';
        form.classList.remove('was-validated');
      }else {
        form.classList.add('was-validated');
      }
    },
    getFile() {
      this.registrant.avatar = this.$refs.avatar.files[0].name;
    }
  },
  mounted() {
    // phone input mask
    const phone = this.$refs.phone;
    const inputMask = new Inputmask('+380 (99) 999-99-99');
    inputMask.mask(phone);
    console.log('test')

    // restrict date to choose in birthday input
    const date = new Date();
    date.setDate(date.getDate() - 1);

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    this.$refs.maxDate.max = year + '-' + month + '-' + day;
  }
}
</script>