<template>
  <h3 class="text-center mb-4">Edit dish</h3>
  <div class="container">
    <form @submit.prevent="submitChange"
          ref="regForm"
          class="needs-validation"
          id="register-form"
          novalidate>
      <div class="row">
        <div class="col-md-5 col-sm-12">
          <img class="img-fluid" :src="imgUrl" alt="dish">
          <input class="form-control my-2"
                 type="file"
                 accept=".jpg, .jpeg, .png"
                 @change="handleFileChange"
          >
        </div>
        <div class="col-md-7 col-sm-12">
          <div class="col-12">
            <div class="mb-3">
              <label for="name" class="form-label">Dish Name</label>
              <input class="form-control"
                     v-model="dish.name"
                     id="name"
                     placeholder="Enter name"
                     pattern="^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+"
                     required
              >
              <div class="invalid-feedback">
                Please enter dish name
              </div>
            </div>
          </div>
          <div class="col-12">
            <div class="mb-3">
              <label for="description" class="form-label">Description</label>
              <textarea class="form-control"
                        v-model="dish.description"
                        id="description"
                        maxlength="512"
                        rows="10"
                        required>
            </textarea>
              <div class="invalid-feedback">
                Please enter description (max 512 signs)
              </div>
            </div>
          </div>
          <div class="container-fluid col-12">
            <div class="row">
              <div class="col-6">
                <div class="mb-3">
                  <label class="form-label" for="category">Category</label>
                  <select class="form-select" id="dish"
                          v-model="dish.dishTypeId">
                    <option v-for="dishType in dishTypes"
                            :key="dishType.id"
                            :value="dishType.id">
                      {{ dishType.name }}
                    </option>
                  </select>
                  <div class="invalid-feedback" id="phone-validation">
                    Please choose dish category
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="mb-3">
                  <label for="price" class="form-label">Price</label>
                  <input class="form-control"
                         v-model="dish.price"
                         id="price"
                         type="text" pattern="[0-9]*[.,]?[0-9]+"
                         min="0"
                         required
                  >
                  <div class="invalid-feedback">
                    Please enter price
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="d-grid mt-4">
        <button type="submit" class="btn btn-primary">Submit</button>
      </div>
    </form>
  </div>
</template>

<script>
import helpers from "@/mixins/helpers";
import request from "@/api/requestConstructor";
import { compare } from "fast-json-patch";

export default {
  mixins: [helpers, request],

  data() {
    return {
      dish: {
        dishType: {
          id: '',
          name: '',
        }
      },
      dishCopy: null,
      dishTypes: [],
      selectedFile: null,
      imgUrl: '',
    }
  },
  methods: {
    handleFileChange(event) {
      const file = event.target.files[0];
      if (file) {
        this.selectedFile = file;
        this.imgUrl = URL.createObjectURL(this.selectedFile)
      }
    },
    async submitChange() {
      if (this.validateForm()) {

        if (JSON.stringify(this.dishCopy) !== JSON.stringify(this.dish)) {
          const patch = compare(this.dishCopy, this.dish);
          try {
            await request.patch(`dishes/${this.dish.id}`, patch, true);
            this.alertSuccess('Applied changes successfully');
          } catch (error) {
            this.alertDanger('Failed to patch dish');
          }
        }

        if (this.selectedFile != null) {
          const formData = new FormData();
          formData.append('image', this.selectedFile);
          try {
            await request.post(`dishes/img/${this.dish.id}`,
                formData, true)
            this.alertSuccess('Applied changes successfully');
          } catch (error) {
            this.alertDanger('Failed to change image');
          }
        }
      }
    }
  },
  async created() {
    const id = this.$route.params.id;
    try {
      this.dish = (await request.get('dishes/' + id)).data;
      this.dishCopy = JSON.parse(JSON.stringify(this.dish));

      this.dishTypes = (await request.get('dishes/types')).data;
      this.imgUrl = this.dish.imageUrl;
    } catch (error) {
      this.alertDanger('Failed to load dish or dishTypes');
    }
  }
}
</script>