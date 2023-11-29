<template>

  <confirmation-modal
      :modal-type="'delete'"
      :item-to-confirm="lineToRemoveName"
      @confirmationDelete="removeLine">
  </confirmation-modal>

<!--  Add cart line modal-->

  <div class="modal fade" id="addModal" tabindex="-1" aria-labelledby="addModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="modal-title fs-5" id="exampleModalLabel">Confirm</h3>
        </div>
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-12">
                <h3 class="text-center">Add new cart line</h3>

                <label for="dish">Select dish</label>
                <select class="form-select" id="dish"
                      v-model="newLine.id">
                  <option v-for="dish in allDishes"
                          :key="dish.id"
                          :value="dish.id">
                    {{dish.name}}
                  </option>
                </select>
                <label for="quantity" class="mt-3">Select quantity</label>
                <input type="number" id="quantity"
                      class="form-control"
                      min="1"
                      v-model="newLine.quantity">

                <div class="d-grid mt-4">
                  <button type="button" class="btn btn-primary"
                          data-bs-dismiss="modal"
                          @click="submitAddLine">
                    Add
                  </button>
                  <button type="button" class="btn btn-secondary mt-2"
                          data-bs-dismiss="modal">
                    Discard
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

<!--  order info-->

  <form @submit.prevent="submit"
        ref="regForm"
        class="needs-validation"
        id="register-form"
        novalidate>
    <div class="container-fluid">
      <div class="row">
        <h3 class="text-md-center text-start mt-2 mb-4">Order id: {{order.id}}</h3>
      </div>
    </div>
    <div class="row">
      <div class="col-lg-5 col-md-12">
        <div class="container-fluid">
          <div class="row">
            <div class="col-6">
              <div class="mb-3">
                <label for="name" class="form-label">Customer Name</label>
                <input class="form-control"
                       v-model="order.customerName"
                       id="name"
                       placeholder="Enter name"
                       pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                       required
                >
                <div class="invalid-feedback">
                  Please enter your name
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="mb-3">
                <label for="surname" class="form-label">Customer Surname</label>
                <input class="form-control"
                       v-model="order.customerSurname"
                       id="surname"
                       placeholder="Enter surname"
                       pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                       required
                >
                <div class="invalid-feedback">
                  Please enter surname
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="mb-3">
                <label class="form-label" for="phone">Enter your phone</label>
                <input class="form-control phone-input"
                       v-model="order.phoneNumber"
                       id="phone"
                       ref="phone"
                       type="tel"
                       pattern="^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$"
                       required
                >
                <div class="invalid-feedback" id="phone-validation">
                  Please enter your phone number
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="mb-3">
                <label for="city" class="form-label">City</label>
                <input class="form-control"
                       v-model="order.city"
                       id="city"
                       placeholder="Enter city"
                       pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                       required
                >
                <div class="invalid-feedback">
                  Please enter city
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="mb-3">
                <label for="street" class="form-label">Street</label>
                <input class="form-control"
                       v-model="order.street"
                       id="street"
                       placeholder="Enter street"
                       pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                       required
                >
                <div class="invalid-feedback">
                  Please enter street
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="mb-3">
                <label for="houseNumber" class="form-label">House Number</label>
                <input class="form-control"
                       v-model="order.houseNumber"
                       type="number"
                       id="houseNumber"
                       placeholder="Enter houseNumber"
                       min="1"
                       required
                >
                <div class="invalid-feedback">
                  Please enter houseNumber
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="col-lg-7 col-md-12">

          <table class="table table-sm align-middle">
            <thead>
            <tr>
              <th class="flex-grow-1">Name</th>
              <th>Quantity</th>
              <th>Price</th>
              <th>Subtotal</th>
              <th></th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="(line, i) in order.cartLines"
                :key="i">
              <td>{{ line.dish.name }}</td>
              <td>
                <input class="form-control"
                       v-model="line.quantity"
                       type="number"
                       min="1"
                       style="width: 5rem;"
                       required
                >
              </td>
              <td>{{formatCurrency(parseFloat(line.dish.price))}}</td>
              <td>{{formatCurrency(parseFloat(line.quantity * line.dish.price))}}</td>
              <td>
                <a @click="confirmRemoveLine(line.id)" class="btn btn-danger px-2 py-1"
                   data-bs-toggle="modal" data-bs-target="#confirmModal">
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </a>
              </td>
            </tr>
            </tbody>
          </table>
        </div>
      </div>
    <div class="fs-4 mt-3">
      <strong>Total: </strong>{{formatCurrency(parseFloat(total()))}}
    </div>
    <div class="d-flex my-4">
      <button class="btn btn-primary flex-grow-1 me-1" type="submit">
        <font-awesome-icon :icon="['fas', 'floppy-disk']" class="me-2"/>Apply changes
      </button>
      <a class="btn btn-primary flex-grow-1"
         data-bs-toggle="modal" data-bs-target="#addModal"
      >
        <font-awesome-icon :icon="['fas', 'plus']" class="me-2"/>Add cart line
      </a>
      <router-link class="btn btn-secondary flex-grow-1 ms-1" to="/admin/orders">
       Back to Orders
      </router-link>
    </div>
  </form>
</template>

<script>
import request from "@/api/requestConstructor"
import helpers from "@/mixins/helpers";
import ConfirmationModal from "@/components/UI/ConfirmationModal.vue";
import { compare } from "fast-json-patch";

export default {
  components: {ConfirmationModal},
  mixins: [request, helpers],

  data() {
    return {
      order: {},
      orderCopy: {},
      newLine: {
        id: 1,
        quantity: 1,
      },
      allDishes: [],

      lineToRemoveName: '',
      lineToRemoveId: ''
    }
  },
  methods: {
    async submit() {
      if (this.validateForm()) {
        const patchDoc = compare(this.orderCopy, this.order);
        try {
          await request.patch(`orders/patch/${this.order.id}`,
              patchDoc, true);
          this.orderCopy = JSON.parse(JSON.stringify(this.order));
          this.alertSuccess('Successfully updated order');
        } catch (error) {
          this.alertDanger('Failed to save changes')
        }
        console.log(JSON.stringify(patchDoc));
      }
    },
    confirmRemoveLine(id) {
      this.lineToRemoveName = this.order.cartLines.filter(l => l.id === id)[0].dish.name
      this.lineToRemoveId = id;
      console.log(`confirm to remove: ${id}`);
    },
    async removeLine() {
      const id = this.lineToRemoveId;
      try {
        await request.delete(`orders/remove/line/${id}`, {}, true)
        this.order.cartLines = this.order.cartLines.filter(l => l.id !== id);
        this.orderCopy = JSON.parse(JSON.stringify(this.order));

      } catch (error) {
        this.alertDanger('Failed to remove line with id: ' + id);
      }
    },
    async submitAddLine() {
      try {
        await request.post('orders/add/line', {
          orderId: this.order.id,
          dishId: this.newLine.id,
          quantity: this.newLine.quantity
        }, true);
        this.order = (await request.get('orders/' + this.order.id)).data;
        this.orderCopy = JSON.parse(JSON.stringify(this.order));

        this.alertSuccess('Successfully added new cart line')
      } catch (error) {
        this.alertDanger('Failed to add line')
      }
    },
    total() {
      return this.order.cartLines?.reduce((sum, line) => {
        return sum + line.quantity * line.dish.price;
      }, 0)
    }
  },
  async created() {
    const id = this.$route.params.id;
    try {
      this.order = (await request.get(
          'orders/' + id)).data;
      this.orderCopy = JSON.parse(JSON.stringify(this.order));

      this.allDishes = (await request.get( // int max value to get all dishes
          'dishes', {perPage: 2147483647})).data.dishes;
    } catch (error) {
      this.alertDanger('Failed to load order or dishes');
    }
  }
}
</script>