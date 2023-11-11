<template>
  <restaurant-layout :pagination-required="false">
    <cart-layout></cart-layout>
  </restaurant-layout>
</template>

<script>
  import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
  import CartLayout from "@/components/restaurantLayout/cartLayout/CartLayout.vue";

  export default {
    components: {CartLayout, RestaurantLayout}
  }
</script>