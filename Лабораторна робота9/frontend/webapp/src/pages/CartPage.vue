<template>
  <restaurant-layout
      :pagination-required="false">
    <cart-layout></cart-layout>
  </restaurant-layout>

</template>

<script setup>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import CartLayout from "@/components/restaurantLayout/cartLayout/CartLayout.vue";
</script>