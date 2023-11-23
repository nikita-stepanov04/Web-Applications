<template>
  <restaurant-header
      @toggleNavbar="toggleNavbar"
      @changeCategoryTo="id => toMenu(id)"
      @search="(args) => console.log(args)"
      :dish-types="$store.state.dishTypes"
  >
  </restaurant-header>
  <div class="page mt-6">
    <div class="d-flex">
      <div class="sidebar hiding hidden" ref="sidebar">
        <restaurant-navbar
            @changeCategoryTo="id => toMenu(id)"
            :dish-types="$store.state.dishTypes"
        >
        </restaurant-navbar>
      </div>
      <div class="expandable">
        <div class="container" ref="expandableContainer">
          <my-account-layout></my-account-layout>
        </div>
      </div>
    </div>
    <restaurant-footer></restaurant-footer>
  </div>

</template>

<script>
import RestaurantHeader from "@/components/restaurantLayout/RestaurantHeader.vue";
import RestaurantNavbar from "@/components/restaurantLayout/RestaurantNavbar.vue";
import RestaurantFooter from "@/components/restaurantLayout/RestaurantFooter.vue";
import MyAccountLayout from "@/components/restaurantLayout/myAccountLayout/MyAccountLayout.vue";

export default {
  components: {
    MyAccountLayout,
    RestaurantFooter,
    RestaurantNavbar,
    RestaurantHeader
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
    },
    toMenu(id) {
      this.$store.dispatch('fetchDishes', {dishTypeId: id});
      this.$router.push('/menu');
    }
  }
}
</script>