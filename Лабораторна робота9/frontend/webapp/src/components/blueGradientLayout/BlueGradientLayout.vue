<template>
  <div class="page bg-lightblue">
    <div class="container">
      <div class="col-lg-6 offset-lg-3 col-sm-10 offset-sm-1">
        <div class="main-block my-5">
          <slot></slot>
        </div>
      </div>
    </div>
    <restaurant-footer></restaurant-footer>
  </div>

</template>

<script setup>
import RestaurantFooter from "@/components/restaurantLayout/RestaurantFooter.vue";
</script>