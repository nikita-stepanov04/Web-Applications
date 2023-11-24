<template>
  <restaurant-layout
      :pagination-required="false">
    <div class="main-block orders mt-4 mx-sm-5 mb-5">
      <form @submit.prevent="submitForm"
            ref="regForm"
            class="needs-validation"
            id="register-form"
            novalidate>
        <div class="container-fluid">
          <div class="row">
            <h3 class="text-md-center text-start mt-2 mb-4">My Order</h3>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-6 col-xs-12">
            <div class="mb-3">
              <label for="name" class="form-label">Name</label>
              <input class="form-control"
                     v-model="order.name"
                     id="name"
                     placeholder="Enter name"
                     pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                     required
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
                     v-model="order.surname"
                     id="surname"
                     placeholder="Enter surname"
                     pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                     required
              >
              <div class="invalid-feedback">
                Please enter surname
              </div>
            </div>
          </div>
          <div class="col-sm-6 col-xs-12">
            <div class="mb-3">
              <label class="form-label" for="phone">Enter your phone</label>
              <input class="form-control phone-input"
                     v-model="order.phoneNumber"
                     id="phone"
                     ref="phone"
                     type="tel"
                     pattern="^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$"
                     required
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
                     v-model="order.city"
                     id="city"
                     placeholder="Enter city"
                     pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                     required
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
                     v-model="order.street"
                     id="street"
                     placeholder="Enter street"
                     pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                     required
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
                     v-model="order.flat"
                     type="number"
                     id="flat"
                     placeholder="Enter flat"
                     min="1"
                     required
              >
              <div class="invalid-feedback">
                Please enter flat
              </div>
            </div>
          </div>
        </div>
        <div class="fs-4 mt-3">
          <strong>Total: </strong>{{formatCurrency($store.getters.total)}}
        </div>
        <div class="d-flex my-4">
          <button class="btn btn-success flex-grow-1 me-1" type="submit">Order</button>
          <router-link class="btn btn-secondary flex-grow-1 ms-1" to="/cart">
            Back to cart
          </router-link>
        </div>
      </form>
    </div>
  </restaurant-layout>
</template>

<script>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor"

export default {
  components: {RestaurantLayout},
  mixins: [helpers, request],

  data() {
    return {
      order: {
        name: '',
        surname: '',
        city: '',
        street: '',
        flat: '',
        phoneNumber: ''
      }
    }
  },
  methods: {
    submitForm() {
      if (this.validateForm()) {
        console.log('valid')
      }
    }
  },
  mounted() {
    this.addPhoneMask(this.$refs.phone)
  },
  async created() {
    const userRole = this.$store.state.user
        ?? await this.$store.dispatch('checkUserAuthorization');
    if (userRole) {
      const userInfo = (await request.get(
          'auth/user-info', {}, true)).data;
      this.order.name = userInfo.name;
      this.order.surname = userInfo.surname;
      this.order.city = userInfo.city;
      this.order.street = userInfo.street;
      this.order.flat = userInfo.flat;
      this.order.phoneNumber = userInfo.phoneNumber;
    }
  }
}
</script>