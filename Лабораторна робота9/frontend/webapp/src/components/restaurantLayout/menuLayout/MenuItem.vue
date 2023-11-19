<template>
  <div class="col-xl-4 col-lg-6 col-md-6 col-sm-6 col-xs-12 mb-3">
    <div class="card dishes-card">
      <a href="#">
        <img :src="dish.imageUrl" :alt="dish.name" class="card-img-top">
      </a>
      <div class="card-body">
        <div class="d-flex">
          <div class="p-2 card-text flex-grow-1">{{ dish.name }}</div>
          <button class="btn btn-outline-primary text-nowrap"
                  @click="buy(dish.id)">
            {{ formatCurrency(dish.price) }}
            <font-awesome-icon :icon="['fas', 'cart-shopping']"/>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
import helpers from "@/mixins/helpers"

export default {
  components: {FontAwesomeIcon},
  mixins: [helpers],
  emits: ['buy'],

  props: {
    dish: Object
  },
  methods: {
    buy(id) {
      this.$emit('buy', id);
      const cart = document.getElementById('cart');
      cart.classList.add('shake-animation');
      setTimeout(() => {
        cart.classList.remove("shake-animation");
      }, 500)
    }
  }
}
</script>