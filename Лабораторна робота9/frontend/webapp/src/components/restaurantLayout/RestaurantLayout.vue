<template>
  <dismissible-alert ref="dismissibleAlert"></dismissible-alert>
  <restaurant-header
      @toggleNavbar="toggleNavbar"
      @changeCategoryTo="id => toMenu({id: id})"
      @search="substring => toMenu({substring: substring})"
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
          <slot></slot>
        </div>
      </div>
    </div>
    <pagination-component
        v-if="paginationRequired"
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
import DismissibleAlert from "@/components/UI/DismissibleAlert.vue";

export default {
  components: {
    PaginationComponent,
    RestaurantFooter,
    RestaurantNavbar,
    RestaurantHeader,
    DismissibleAlert
  },

  props: {
    paginationRequired: Boolean,
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
    toMenu({id, substring}) {
      this.$store.dispatch('fetchDishes', !substring
        ? {dishTypeId: id}
        : {substring: substring});
      this.$router.push('/menu');
    }
  }
}
</script>