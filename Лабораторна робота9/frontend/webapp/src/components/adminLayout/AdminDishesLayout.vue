<template>
  <confirmation-modal
      :modal-type="'delete'"
      :item-to-confirm="dishToRemoveName"
      @confirmationDelete="remove">
  </confirmation-modal>

  <div class="d-flex mb-4">
    <h3 class="text-center flex-grow-1">Update dishes</h3>
    <router-link to="/admin/dish/add" class="btn btn-primary">
      <font-awesome-icon :icon="['fas', 'plus']" class="me-2"/>Add dish
    </router-link>
  </div>
  <div class="row">
    <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6 col-xs-12 mb-3"
         v-for="dish in dishes" :key="dish.id">
      <div class="card dishes-card">
        <img :src="dish.imageUrl" :alt="dish.name" class="card-img-top">
        <div class="card-body fs-5">
          <div class="d-grid">
            <div class="p-2"><strong>Name: </strong>{{ dish.name }}</div>
            <div class="p-2"><strong>Price: </strong> {{ formatCurrency(dish.price) }}</div>
            <router-link :to="{name: 'edit-dish', params: {id: dish.id}}"
                         class="btn btn-primary mt-2">
              Edit
            </router-link>
            <a @click="confirmRemove(dish.id, dish.name)"
               class="btn btn-danger mt-2"
               data-bs-toggle="modal" data-bs-target="#confirmModal">
              Delete
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import request from "@/api/requestConstructor"
import helpers from "@/mixins/helpers";
import ConfirmationModal from "@/components/UI/ConfirmationModal.vue";

export default {
  components: {ConfirmationModal},
  mixins: [helpers, request],

  data() {
    return {
      dishes: [],
      dishToRemoveName: '',
      dishToRemoveId: '',
    }
  },
  methods: {
    confirmRemove(id, name) {
      this.dishToRemoveName = name;
      this.dishToRemoveId = id;
    },
    async remove() {
      try {
        await request.delete(`dishes/${this.dishToRemoveId}`, {}, true)
        this.dishes = this.dishes.filter(d => d.id !== this.dishToRemoveId);

      } catch (error) {
        this.alertDanger(`Failed to remove dish: ${this.dishToRemoveName}`);
      }
    }
  },
  async created() {
    try {
      this.dishes = (await request.get( // int max value to get all dishes
          'dishes', {perPage: 2147483647})).data.dishes;
    } catch (error) {
      this.alertDanger('Failed to load order or dishes');
    }
  }
}
</script>