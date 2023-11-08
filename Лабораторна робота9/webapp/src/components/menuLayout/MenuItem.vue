<template>
  <div class="col-xl-4 col-lg-6 col-md-6 col-sm-10 col-xs-12 offset-md-0 offset-sm-1 offset-xs-0  mb-3">
    <div class="card dishes-card">
      <a href="#" class="main-link">
        <img :src="require(`@/assets/img/${imageUrl}`)" :alt="name" class="card-img-top">
        <div class="card-body">
          <div class="d-flex">
            <div class="p-2 card-text flex-grow-1">{{name}}</div>
            <button type="submit" class="btn btn-outline-primary">
              {{ formatCurrency(price) }}
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

  export default {
    components: {FontAwesomeIcon},
    props: {
      name: String,
      price: Number,
      imageUrl: String
    },
    methods: {
      formatCurrency(price) {
        return '₴ ' + price.toFixed(2)
            .replace(/\d(?=(\d{3})+\.)/g, '$&,');
      }
    }
  }
</script>