<template>
  <confirmation-modal
      :modal-type="'delete'"
      :item-to-confirm="typeToRemoveName"
      @confirmationDelete="removeDishType">
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
                <h3 class="text-center">Add new dish type</h3>

                <label for="name" class="mt-3">Enter name</label>
                <input id="name"
                       class="form-control"
                       maxlength="30"
                       v-model="newDishName">

                <div class="d-grid mt-4">
                  <button type="button" class="btn btn-primary"
                          data-bs-dismiss="modal"
                          @click="submitAddDish">
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

<!--  edit dish types -->

  <div class="container">
    <div class="row justify-content-center">
      <h3 class="text-center mb-4">Edit dish types</h3>
      <div class="col-lg-6 col-md-8">
        <form @submit.prevent="saveChanges"
              ref="regForm"
              class="needs-validation"
              id="register-form"
              novalidate>
          <table class="table table-striped table-bordered">
            <thead>
            <tr class="align-middle">
              <th>Id</th>
              <th>Name</th>
              <th class="text-center">
                <a class="btn btn-primary"
                   data-bs-toggle="modal" data-bs-target="#addModal">
                  <font-awesome-icon :icon="['fas', 'plus']" />
                </a>
              </th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="dishType in dishTypes" :key="dishType.id">
              <td>{{dishType.id}}</td>
              <td>
                <input class="form-control"
                       v-model="dishType.name"
                       id="name"
                       minlength="1"
                       required>
                <div class="invalid-feedback text-center">
                  Please enter dish name
                </div>
              </td>
              <td class="text-center">
                <a @click="confirmRemoveDishType(dishType.id)" class="btn btn-danger"
                   data-bs-toggle="modal" data-bs-target="#confirmModal">
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </a>
              </td>
            </tr>
            </tbody>
          </table>
          <div class="d-grid">
            <button class="btn btn-primary" type="submit">
              Submit changes
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>

</template>

<script>
import ConfirmationModal from "@/components/UI/ConfirmationModal.vue";
import request from "@/api/requestConstructor";
import helpers from "@/mixins/helpers";
import { compare } from "fast-json-patch";

export default {
  mixins: [helpers, request],
  components: {ConfirmationModal},

  data() {
    return {
      dishTypes: [],
      dishTypesCopy: null,
      typeToRemoveId: '',
      typeToRemoveName: '',

      newDishName: '',
    }
  },
  methods: {
    confirmRemoveDishType(id) {
      this.typeToRemoveId = id;
      this.typeToRemoveName = this.dishTypes.filter(t => t.id === id)[0].name;
    },
    async removeDishType() {
      try {
        await request.delete(
            `dishes/delete/dish-type/${this.typeToRemoveId}`,
            {}, true);
        this.dishTypes = this.dishTypes.filter(d => d.id !== this.typeToRemoveId);
        this.dishTypesCopy = this.dishTypesCopy.filter(d => d.id !== this.typeToRemoveId);
      } catch (error) {
        this.alertDanger(`Failed to remove dish type: ${this.typeToRemoveName}`);
      }
    },
    async submitAddDish() {
      if (this.newDishName) {
        try {
          await request.post('dishes/add/dish-type',
              this.newDishName, true, {
                'Content-Type': 'application/json'
              });
          this.alertSuccess('Successfully added dish type');
          await this.fetchData();
        } catch (error) {
          this.alertDanger('Failed to add dish type')
        }
      } else {
        this.alertWarning('Dish name was not provided')
      }
    },
    async saveChanges() {
      console.log('asdf')
      if (this.validateForm()) {
        if (JSON.stringify(this.dishTypes) !== JSON.stringify(this.dishTypesCopy)) {
          const patch = compare(this.dishTypesCopy, this.dishTypes);
          await request.patch('dishes/patch/dish-types', patch, true);
          this.dishTypesCopy = JSON.parse(JSON.stringify(this.dishTypes));
          this.alertSuccess('Successfully saved changes')
        } else {
          this.alertWarning('Nothing to save')
        }
      }
    },
    async fetchData() {
      try {
        this.dishTypes = (await request.get('dishes/types')).data;
        this.dishTypesCopy = JSON.parse(JSON.stringify(this.dishTypes));
      } catch (error) {
        this.alertDanger('Failed to load dish or dishTypes');
      }
    }
  },
  async created() {
    await this.fetchData();
  }
}
</script>