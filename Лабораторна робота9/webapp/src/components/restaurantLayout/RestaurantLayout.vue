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
         <slot :dishes="dishes"></slot>
       </div>
      </div>
    </div>
    <pagination-component class="mb-3"
        @change-page="args => console.log(args)"
        v-bind:currant-page="pagination.currantPage"
        v-bind:total-pages="pagination.totalPages">
    </pagination-component>
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
      dishes: [
        {
          name: "First pizza",
          imageUrl: "pizza-1.jpg",
          price: 45.00
        },
        {
          name: "Second pizza",
          imageUrl: "pizza-2.jpg",
          price: 65.00
        },
        {
          name: "First burger",
          imageUrl: "burger-1.jpg",
          price: 45.00
        },
        {
          name: "Second burger",
          imageUrl: "burger-2.jpg",
          price: 55.00
        },
        {
          name: "First soup",
          imageUrl: "soup-1.jpg",
          price: 70.00
        },
        {
          name: "Second soup",
          imageUrl: "soup-2.jpg",
          price: 65.00
        }
      ],
      dishTypes: [
        {name: 'Burgers', checked: false},
        {name: 'Salads', checked: false},
        {name: 'Pizzas', checked: true},
        {name: 'Soups', checked: false}
      ],
      pagination: {
        currantPage: 1,
        totalPages: 3
      },
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