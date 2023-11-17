<template>
  <h3 class="text-center my-2">Choose your dishes</h3>
  <div class="row">
    <menu-item
        v-for="(dish, key) in dishes"
        :key="key"
        v-bind:dish="dish"
        @buy="id => buy(id)">
    </menu-item>
  </div>
</template>

<script>
 import MenuItem from "@/components/restaurantLayout/menuLayout/MenuItem.vue";

 export default {
   components: {MenuItem},

   props: {
    dishes: Array
   },
   methods: {
     buy(dishId) {
       let orders = this.$store.state.orders;
       const order = orders.filter(o => o.dishId === dishId)[0];
       if (order != null) {
         order.quantity++;
       } else {
         const dish = this.$store.state.dishes.filter(d => d.id === dishId)[0];
         const order = {
           dishId: dishId,
           quantity: 1,
           name: dish.name,
           price: dish.price
         }
         this.$store.state.orders.push(order);
       }
       this.$cookies.set('orders', JSON.stringify(this.$store.state.orders), '1d');
     }
   }
 }
</script>