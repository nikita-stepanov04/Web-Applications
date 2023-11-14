<template>
  <h3 class="text-center my-2">Choose your dishes</h3>
  <div class="row">
    <menu-item
        v-for="(dish, key) in dishes"
        :key="key"
        v-bind:name="dish.name"
        v-bind:price="dish.price"
        v-bind:image-url="dish.imageUrl">
    </menu-item>
  </div>
</template>

<script>
 import MenuItem from "@/components/restaurantLayout/menuLayout/MenuItem.vue";

 export default {
   components: {MenuItem},

   props: {
    dishes: Array
   }
 }
</script>