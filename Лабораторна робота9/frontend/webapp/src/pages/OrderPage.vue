<template>
  <restaurant-layout
      :pagination-required="false">
    <order-layout></order-layout>
  </restaurant-layout>
</template>

<script setup>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import OrderLayout from "@/components/restaurantLayout/orderLayout/OrderLayout.vue";
</script>