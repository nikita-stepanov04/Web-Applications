<template>
  <restaurant-header
      @toggleNavbar="toggleNavbar"
      @changeCategoryTo="(args) => console.log(args)"
      @search="(args) => console.log(args)"
      :items-in-cart="itemsInCart"
      :dish-types="$store.state.dishTypes"
  >
  </restaurant-header>
  <div class="page">
    <div class="d-flex">
      <div class="sidebar hiding hidden" ref="sidebar">
        <restaurant-navbar
            @changeCategoryTo="(args) => console.log(args)"
            :dish-types="$store.state.dishTypes"
        >
        </restaurant-navbar>
      </div>
      <div class="expandable">
        <div class="container" ref="expandableContainer">
          <cart-layout></cart-layout>
        </div>
      </div>
    </div>
<!--    <pagination-component-->
<!--        class="mb-3"-->
<!--        :pagination-info="paginationInfo"-->
<!--        @change-page="args => console.log(args)"-->
<!--    >-->
<!--    </pagination-component>-->
    <restaurant-footer></restaurant-footer>
  </div>

</template>

<script>
import RestaurantHeader from "@/components/restaurantLayout/RestaurantHeader.vue";
import RestaurantNavbar from "@/components/restaurantLayout/RestaurantNavbar.vue";
import RestaurantFooter from "@/components/restaurantLayout/RestaurantFooter.vue";
//import PaginationComponent from "@/components/UI/PaginationComponent.vue";
import CartLayout from "@/components/restaurantLayout/cartLayout/CartLayout.vue";

export default {
  components: {
    CartLayout,
    //PaginationComponent,
    RestaurantFooter,
    RestaurantNavbar,
    RestaurantHeader
  },

  data() {
    return {
      paginationInfo: {
        pageNumber: 1,
        itemsPerPage: 6,
        totalPages: 3,
        dishTypeId: null
      },
      itemsInCart: 10,
    }
  },
  created() {
    this.$store.dispatch('fetchDishTypes');
  },
  mounted() {
    window.addEventListener('resize', this.hideSidebarOnNarrowScreen);
    this.hideSidebarOnNarrowScreen();
  },
  methods: {
    toggleNavbar() {
      const sidebar = this.$refs.sidebar;
      sidebar.classList.toggle('hidden');
    },
    hideSidebarOnNarrowScreen() {
      const sidebar = this.$refs.sidebar;
      window.innerWidth <= 800
          ? sidebar?.classList?.add('hidden')
          : sidebar?.classList?.remove('hidden');
    }
  }
}
</script>