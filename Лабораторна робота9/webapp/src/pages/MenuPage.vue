<template>
  <restaurant-layout v-slot="{dishes}">
    <menu-layout :dishes="dishes"></menu-layout>
  </restaurant-layout>
</template>

<script>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import MenuLayout from "@/components/restaurantLayout/menuLayout/MenuLayout.vue";

export default {
  components: {MenuLayout, RestaurantLayout}
}
</script>