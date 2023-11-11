<template>
  <restaurant-layout :pagination-required="true">
    <menu-layout></menu-layout>
  </restaurant-layout>
</template>

<script>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import MenuLayout from "@/components/restaurantLayout/menuLayout/MenuLayout.vue";

export default {
  components: {MenuLayout, RestaurantLayout}
}
</script>