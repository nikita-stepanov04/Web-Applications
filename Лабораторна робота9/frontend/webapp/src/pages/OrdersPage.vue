<template>
  <restaurant-layout
      :pagination-required="false">
    <orders-layout></orders-layout>
  </restaurant-layout>
</template>

<script setup>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import OrdersLayout from "@/components/restaurantLayout/orderLayout/OrdersLayout.vue"
</script>