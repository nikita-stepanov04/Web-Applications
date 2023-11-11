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

   data() {
     return {
       dishes: [
         {
           id: 0,
           name: "First pizza",
           imageUrl: "pizza-1.jpg",
           price: 45.00
         },
         {
           id: 1,
           name: "Second pizza",
           imageUrl: "pizza-2.jpg",
           price: 65.00
         },
         {
           id: 2,
           name: "First burger",
           imageUrl: "burger-1.jpg",
           price: 45.00
         },
         {
           id: 3,
           name: "Second burger",
           imageUrl: "burger-2.jpg",
           price: 55.00
         },
         {
           id: 4,
           name: "First soup",
           imageUrl: "soup-1.jpg",
           price: 70.00
         },
         {
           id: 5,
           name: "Second soup",
           imageUrl: "soup-2.jpg",
           price: 65.00
         }
       ],
     }
   }
 }
</script>