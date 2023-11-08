<template>
  <restaurant-header @toggleNavbar="toggleNavbar"></restaurant-header>
  <div class="page">
    <div class="container-fluid">
      <div class="row justify-content-center">
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 sidebar"
             v-if="navbarVisible">
          <restaurant-navbar></restaurant-navbar>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 test">
          <slot></slot>
        </div>
        <pagination-component class="mb-3"
            @change-page="args => console.log(args)"
            v-bind:currant-page="pagination.currantPage"
            v-bind:total-pages="pagination.totalPages">
        </pagination-component>
      </div>
    </div>
  </div>
  <restaurant-footer></restaurant-footer>
</template>

<script>
import RestaurantHeader from "@/components/restaurantLayout/RestaurantHeader.vue";
import RestaurantNavbar from "@/components/restaurantLayout/RestaurantNavbar.vue";
import RestaurantFooter from "@/components/restaurantLayout/RestaurantFooter.vue";
import PaginationComponent from "@/components/UI/PaginationComponent.vue";

export default {
  components: {
    PaginationComponent,
    RestaurantFooter,
    RestaurantNavbar,
    RestaurantHeader
  },

  data() {
    return {
      pagination: {
        currantPage: 1,
        totalPages: 3
      },
      navbarVisible: true
    }
  },
  methods: {
    toggleNavbar() {
      this.navbarVisible = !this.navbarVisible;
    }
  }
}
</script>

<style scoped>
.navbar-enter-active,
.navbar-leave-active {
  transition: all .3s ease;
}

.navbar-enter-from,
.navbar-leave-to {
  transform: translateX(-100%);
}
</style>