<template>
  <restaurant-layout
      :pagination-required="false">
    <div class="main-block orders mt-4 mx-sm-5 mb-5">
      <div class="container-fluid">
        <h3 class="text-center mb-4 mt-4 mt-sm-3">{{ dish?.name }}</h3>
        <div class="row my-5">
          <div class="col-lg-5 col-md-12 mb-4">
            <img :src="dish?.imageUrl" :alt="dish?.name" class="img-fluid rounded-4 shadow">
          </div>
          <div class="col-lg-6 offset-lg-1 col-md-12">
            <p>{{ dish?.description }}</p>
            <div class="d-flex mt-4">
              <div class="me-3">
                <div class="rounded-3  p-2 text-center">
                  <button class="btn btn-outline-secondary px-2 py-1"
                          @click="increaseQuantityBy(-1)">
                    <font-awesome-icon :icon="['fas', 'minus']" />
                  </button>
                  <span class="mx-2 align-middle">{{quantity}}</span>
                  <button class="btn btn-outline-secondary px-2 py-1"
                          @click="increaseQuantityBy(1)">
                    <font-awesome-icon :icon="['fas', 'plus']" />
                  </button>
                </div>
              </div>
              <button class="btn btn-primary text-nowrap flex-grow-1"
                  @click="buy">
                {{ formatCurrency(parseInt(dish?.price)) }}
                <font-awesome-icon :icon="['fas', 'cart-shopping']"/>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </restaurant-layout>
</template>

<script>
import RestaurantLayout from "@/components/restaurantLayout/RestaurantLayout.vue";
import request from "@/api/requestConstructor";
import helpers from "@/mixins/helpers";

export default {
  components: {RestaurantLayout},
  mixins: [request, helpers],

  data() {
    return {
      dish: {},
      quantity: 1
    }
  },
  methods: {
    increaseQuantityBy(n) {
      if (this.quantity > 1 || n > 0 ) {
        this.quantity += n;
      }
    },
    buy() {
      console.log(this.quantity)
      this.$store.commit('buy', {dishId: this.dish.id, quantity: this.quantity})
      this.buyAnimation();
    }
  },

  async created() {
    const dishId = this.$route.params.id
    this.dish = this.$store.state.dishes
        .filter(d => d.id === parseInt(dishId))[0];
    try {
      if (!this.dish) {
        this.dish = (await request.get(`dishes/${dishId}`)).data;
      } else {
        this.dish.description = (await request.get(
            `dishes/description-for/${dishId}`)).data;
      }
      document.title = this.dish.name
    } catch (error) {
      console.log(error)
    }
  }
}
</script>