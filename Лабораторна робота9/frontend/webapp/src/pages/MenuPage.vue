<template>
  <restaurant-header
      @toggleNavbar="toggleNavbar"
      @changeCategoryTo="id => $store.dispatch('fetchDishes', {dishTypeId: id})"
      @search="(args) => console.log(args)"
      :items-in-cart="itemsInCart"
      :dish-types="$store.state.dishTypes"
  >
  </restaurant-header>
  <div class="page">
    <div class="d-flex">
      <div class="sidebar hiding hidden" ref="sidebar">
        <restaurant-navbar
            @changeCategoryTo="id => $store.dispatch('fetchDishes', {dishTypeId: id})"
            :dish-types="$store.state.dishTypes"
        >
        </restaurant-navbar>
      </div>
      <div class="expandable">
        <div class="container" ref="expandableContainer">
          <menu-layout
            :dishes="$store.state.dishes">
          </menu-layout>
        </div>
      </div>
    </div>
    <pagination-component
        class="mb-3"
        :pagination-info="$store.state.dishesPagingInfo"
        @change-page="page => $store.dispatch('fetchDishes', {currentPage: page})"
    >
    </pagination-component>
    <restaurant-footer></restaurant-footer>
  </div>

</template>

<script>
import RestaurantHeader from "@/components/restaurantLayout/RestaurantHeader.vue";
import RestaurantNavbar from "@/components/restaurantLayout/RestaurantNavbar.vue";
import RestaurantFooter from "@/components/restaurantLayout/RestaurantFooter.vue";
import PaginationComponent from "@/components/UI/PaginationComponent.vue";
import MenuLayout from "@/components/restaurantLayout/menuLayout/MenuLayout.vue";

export default {
  components: {
    MenuLayout,
    PaginationComponent,
    RestaurantFooter,
    RestaurantNavbar,
    RestaurantHeader
  },

  data() {
    return {
      itemsInCart: 10,
    }
  },
  created() {
    this.$store.dispatch('fetchDishTypes');
    this.$store.dispatch('fetchDishes', {})
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