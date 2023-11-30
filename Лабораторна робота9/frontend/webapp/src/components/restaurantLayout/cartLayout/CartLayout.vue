<template>
  <confirmation-modal
      :modal-type="'delete'"
      :item-to-confirm="itemToRemoveName"
      @confirmationDelete="remove">
  </confirmation-modal>

  <h3 class="text-center my-2">My orders</h3>
  <div class="row orders">
    <div class="col-lg-9 col-md-9 col-sm-12 cart mb-0">
      <table class="table table-sm table-striped table-light">
        <thead>
        <tr>
          <th>Name</th>
          <th>Price</th>
          <th class="on-mobile-cart-column-hidden">Subtotal</th>
          <th class="text-end">Quantity</th>
          <th class="text-end">
            <a class="btn btn-danger px-2 py-1"
               data-bs-toggle="modal" data-bs-target="#confirmModal"
               @click="confirmRemove({id: 0, name: 'all dishes'})">
              <font-awesome-icon :icon="['fas', 'eraser']"/>
            </a>
          </th>
        </tr>
        </thead>
        <tbody>
        <cart-line
            v-for="cartLine in $store.getters.getCart"
            :cart-line="cartLine"
            :key="cartLine.dishId"
            @confirm-remove="args => confirmRemove(args)">
        </cart-line>
        </tbody>
      </table>
      <pagination-component
          v-if="$store.state.orders.length > 6"
          :pagination-info="$store.state.ordersPagination"
          @change-page="page => $store.commit('changeCartPage', page)"
      >
      </pagination-component>
    </div>
    <cart-summary></cart-summary>
  </div>
</template>

<script>
import CartLine from "@/components/restaurantLayout/cartLayout/CartLine.vue";
import CartSummary from "@/components/restaurantLayout/cartLayout/CartSummary.vue";
import ConfirmationModal from "@/components/UI/ConfirmationModal.vue";
import PaginationComponent from "@/components/UI/PaginationComponent.vue";

export default {
  components: {PaginationComponent, CartSummary, CartLine, ConfirmationModal},

  data() {
    return {
      itemToRemoveId: '',
      itemToRemoveName: ''
    }
  },
  methods: {
    confirmRemove(args) {
      this.itemToRemoveId = args.id;
      this.itemToRemoveName = args.name;
      console.log('item name from layout: ', this.itemToRemoveName)
    },
    remove() {
      this.$store.commit('removeDishFromCart', this.itemToRemoveId);
    }
  }
}
</script>