<template>
  <div class="col-lg-3 col-md-3 col-sm-12 cart-summary">
    <div class="row">
      <div class="col-lg-12 col-md-12 col-sm-12">
        <h4 class="text-white m-2">Cart summary:</h4>
      </div>
      <div class="col-lg-12 col-md-12 col-sm-12">
        <p class="text-white m-2">Total:
          <span class="text-nowrap">{{formatCurrency($store.getters.total)}}</span>
        </p>
      </div>
      <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="d-grid align-self-end mb-2">
          <router-link to="/" class="btn btn-light m-2">Order</router-link>
          <router-link to="/menu" class="btn btn-secondary m-2">Back to dishes</router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import helpers from "@/mixins/helpers"
export default {
  mixins: [helpers]
}
</script>