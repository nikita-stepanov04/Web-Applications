<template>
  <restaurant-layout>
    <menu-layout></menu-layout>
  </restaurant-layout>
</template>

<script>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import MenuLayout from "@/components/menuLayout/MenuLayout.vue";

export default {
  components: {MenuLayout, RestaurantLayout}
}
</script>