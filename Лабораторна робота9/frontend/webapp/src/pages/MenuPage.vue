<template>
  <restaurant-layout
      :pagination-required="true">
    <menu-layout></menu-layout>
  </restaurant-layout>

</template>

<script setup>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import MenuLayout from "@/components/restaurantLayout/menuLayout/MenuLayout.vue";
</script>