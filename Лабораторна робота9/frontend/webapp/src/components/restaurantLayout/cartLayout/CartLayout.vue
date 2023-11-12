<template>
  <h3 class="text-center my-2">My orders</h3>
  <div class="row orders">
    <div class="col-lg-9 col-md-9 col-sm-12 cart mb-0">
      <table class="table table-sm table-striped table-light">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Price</th>
            <th>Subtotal</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <cart-line
              v-for="cartLine in $store.state.orders"
              :cart-line="cartLine"
              :key="cartLine.dishId">
          </cart-line>
        </tbody>
      </table>
    </div>
    <cart-summary></cart-summary>
  </div>
</template>

<script>
import CartLine from "@/components/restaurantLayout/cartLayout/CartLine.vue";
import CartSummary from "@/components/restaurantLayout/cartLayout/CartSummary.vue";

export default {
  components: {CartSummary, CartLine}
}
</script>