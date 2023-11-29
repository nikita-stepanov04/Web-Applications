<template>
  <div class="main-block orders mt-4 mx-sm-5 mb-5">
    <h3 class="text-center mb-3">My Orders</h3>
    <div class="accordion" id="order-accordion"
         v-if="orders.length > 0">
      <div
          v-for="(order, index) in orders"
          :key="index"
          class="accordion-item"
      >
        <h2 class="accordion-header">
          <button class="accordion-button"
                :class="{
                  'collapsed': index !== 0
                }"
                type="button"
                data-bs-toggle="collapse"
                :data-bs-target="'#order-' + index"
                aria-expanded="true"
                :aria-controls="'order-' + index">
            Order dated: {{formatDate(order.purchaseDate)}}
            <is-checked-icon :condition="order.isCompleted" class="mx-2"/>
          </button>
        </h2>
        <div :id="'order-' + index"
             class="accordion-collapse collapse"
             :class="{
                'show': index === 0
             }"
             data-bs-parent="#order-accordion">
          <div class="accordion-body">
            <div class="container-fluid">
              <div class="row">
                <div class="col-lg-5 col-md-12">
                  <div class="d-grid">
                    <h3>
                      <font-awesome-icon
                          :icon="['fas', 'circle-dot']"
                          :style="{'color': colorByNumber(index)}"
                          size="2xs"/>
                      <span class="ms-3">Order Info</span>
                    </h3>
                    <p class="my-0">
                      <span class="fw-semibold">Customer: </span>
                      {{order.customerName}} {{order.customerSurname}}
                    </p>
                    <p class="my-0">
                      <span class="fw-semibold">Delivery: </span>
                      {{order.city}}, {{order.street}}, {{order.houseNumber}}
                    </p>
                    <p class="my-0">
                      <span class="fw-semibold">Ordered: </span>
                      {{ formatDate(order.purchaseDate) }}
                    </p>
                    <p class="my-0">
                      <span class="fw-semibold">Phone: </span>
                      {{ order.phoneNumber }}
                    </p>
                    <p class="my-0">
                      <span class="fw-semibold">Completed: </span>
                      <is-checked-icon :condition="order.isCompleted"/>
                    </p>
                    <p class="mt-2">
                      <span class="h5">Total: </span>
                      {{ formatCurrency(order.totalPrice) }}
                    </p>
                  </div>
                </div>
                <div class="col-lg-7 col-md-12 main-block orders px-3 py-1">
                  <table class="table table-sm table-striped table-light">
                    <thead>
                    <tr>
                      <th>Name</th>
                      <th>Price</th>
                      <th>Quantity</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr v-for="(line, i) in order.cartLines"
                        :key="i">
                      <td>{{ line.dish.name }}</td>
                      <td>{{ formatCurrency(line.dish.price) }}</td>
                      <td>{{ line.quantity }}</td>
                    </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="h5 mt-4 text-danger text-center"
        v-else>
      No orders yet
    </div>
  </div>
</template>

<script>
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor"
import IsCheckedIcon from "@/components/UI/IsCheckedIcon.vue";

export default {
  components: {IsCheckedIcon},
  mixins: [helpers],

  data() {
    return {
      orders: []
    }
  },
  async created() {
    try {
      this.orders = (await request.get(
          'orders', {}, true)).data;
    } catch (error) {
      this.alertDanger('Failed to load orders');
    }
  }
}
</script>