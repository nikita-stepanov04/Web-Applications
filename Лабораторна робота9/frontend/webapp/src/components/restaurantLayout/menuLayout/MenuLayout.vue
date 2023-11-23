<template>
  <h3 class="text-center my-2">Choose your dishes</h3>
  <div class="row">
    <menu-item
        v-for="(dish, key) in $store.state.dishes"
        :key="key"
        v-bind:dish="dish"
        @buy="id => $store.commit('buy', id)">
    </menu-item>
  </div>
</template>

<script>
 import MenuItem from "@/components/restaurantLayout/menuLayout/MenuItem.vue";

 export default {
   components: {MenuItem},
 }
</script>