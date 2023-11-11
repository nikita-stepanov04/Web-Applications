<template>
  <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6 col-xs-12 mb-3">
    <div class="card dishes-card">
      <a href="#" class="main-link">
        <img :src="require(`@/assets/img/${imageUrl}`)" :alt="name" class="card-img-top">
        <div class="card-body">
          <div class="d-flex">
            <div class="p-2 card-text flex-grow-1">{{name}}</div>
            <button type="submit" class="btn btn-outline-primary text-nowrap">
              {{formatCurrency(price)}}
              <font-awesome-icon :icon="['fas', 'cart-shopping']" />
            </button>
          </div>
        </div>
      </a>
    </div>
  </div>
</template>

<script>
  import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
  import helpers from "@/mixins/helpers"

  export default {
    components: {FontAwesomeIcon},
    mixins: [helpers],

    props: {
      name: String,
      price: Number,
      imageUrl: String
    }
  }
</script>