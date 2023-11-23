<template>
  <restaurant-layout
      :pagination-required="false">
    <my-account-layout></my-account-layout>
  </restaurant-layout>

</template>

<script setup>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import MyAccountLayout from "@/components/restaurantLayout/myAccountLayout/MyAccountLayout.vue";
</script>