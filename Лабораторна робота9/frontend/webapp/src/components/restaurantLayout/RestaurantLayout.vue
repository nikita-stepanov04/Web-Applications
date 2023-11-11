<template>
  <restaurant-header
      @toggleNavbar="toggleNavbar"
      @changeCategoryTo="(args) => console.log(args)"
      @search="(args) => console.log(args)"
      :dish-types="dishTypes"
      :items-in-cart="itemsInCart"
  >
  </restaurant-header>
  <div class="page">
    <div class="d-flex">
     <div class="sidebar hiding hidden" ref="sidebar">
       <restaurant-navbar
           :dish-types="dishTypes"
           @changeCategoryTo="(args) => console.log(args)"
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
        class="mb-3"
        v-if="paginationRequired"
        :pagination-info="paginationInfo"
        @change-page="args => console.log(args)"
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

export default {
  components: {
    PaginationComponent,
    RestaurantFooter,
    RestaurantNavbar,
    RestaurantHeader
  },

  props: {
    paginationRequired: Boolean
  },
  data() {
    return {
      paginationInfo: {
        pageNumber: 1,
        itemsPerPage: 6,
        totalPages: 3,
        dishTypeId: null
      },
      dishTypes: [
        {name: 'Burgers', checked: false},
        {name: 'Salads', checked: false},
        {name: 'Pizzas', checked: true},
        {name: 'Soups', checked: false}
      ],
      itemsInCart: 10,
    }
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