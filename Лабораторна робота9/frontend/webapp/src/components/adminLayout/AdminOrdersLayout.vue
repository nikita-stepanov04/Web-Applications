<template>
  <confirmation-modal
      :modal-type="modalType"
      :item-to-confirm="`line with id: ${orderToConfirmId}`"
      @confirmationDelete="remove(orderToConfirmId)"
      @confirmationComplete="completed(orderToConfirmId)">
  </confirmation-modal>

  <table class="table table-light table-bordered table-sm"
      v-if="orders.length > 0">
    <thead>
      <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Delivery</th>
        <th>Phone</th>
        <th>Total</th>
        <th>Dishes</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(order, index) in orders" :key="index" class="text-nowrap">
        <td>{{order.id}}</td>
        <td>{{order.customerName}} {{order.customerSurname}}</td>
        <td>{{order.city}} {{order.street}} {{order.houseNumber}}</td>
        <td>{{order.phoneNumber}}</td>
        <td>{{formatCurrency(parseInt(order.totalPrice))}}</td>
        <td class="p-0">
          <table class="table table-sm table-light">
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
              <td class="text-nowrap">{{formatCurrency(parseInt(line.dish.price))}}</td>
              <td class="text-center">{{ line.quantity }}</td>
            </tr>
            </tbody>
          </table>
        </td>
        <td>
          <div class="container-fluid">
            <div class="row justify-content-center">
              <div class="col-auto mt-1">
                <a @click="confirmCompleted(order.id)" class="btn btn-primary me-1 mt-1 px-2 py-1"
                   data-bs-toggle="modal" data-bs-target="#confirmModal">
                  <font-awesome-icon :icon="['far', 'circle-check']"/>
                </a>
                <router-link :to="{name: 'edit-order', params: {id: order.id}}"
                             class="btn btn-success me-1 mt-1 px-2 py-1">
                  <font-awesome-icon :icon="['fas', 'pen-to-square']"/>
                </router-link>
                <a @click="confirmRemove(order.id)"
                    class="btn btn-danger me-1 mt-1 px-2 py-1"
                   data-bs-toggle="modal" data-bs-target="#confirmModal">
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </a>
              </div>
            </div>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
  <div v-else class="text-center text-danger h4">No new orders yet</div>
</template>

<script>
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor"
import ConfirmationModal from "@/components/UI/ConfirmationModal.vue";

export default {
  components: {ConfirmationModal},
  mixins: [helpers],

  data() {
    return {
      orders: [],
      orderToConfirmId: '',
      modalType: 'delete'
    }
  },
  methods: {
    async confirmCompleted(id) {
      this.modalType = 'complete'
      this.orderToConfirmId = id;
    },
    async completed(id) {
      try {
        await request.get(
            'orders/complete/' + id, {}, true);
        this.orders = this.orders.filter(o => o.id !== id);
        this.alertSuccess('Successfully removed order with id:' + id)
      } catch (error) {
        this.alertDanger('Failed to remove order with id: ' + id);
      }
    },
    confirmRemove(id) {
      this.modalType = 'delete'
      this.orderToConfirmId = id;
    },
    async remove(id) {
      try {
        await request.delete(
            'orders/remove/' + id, {}, true);
        this.orders = this.orders.filter(o => o.id !== id);
        this.alertSuccess('Successfully removed order with id:' + id)
      } catch (error) {
        this.alertDanger('Failed to remove order with id: ' + id)
      }
    },
    async fetch() {
      try {
        this.orders = (await request.get(
            'orders/all-non-completed', {}, true)).data;
      } catch (error) {
        this.alertDanger('Failed to load orders')
      }
    }
  },
  async created() {
    await this.fetch();
  }
}
</script>>