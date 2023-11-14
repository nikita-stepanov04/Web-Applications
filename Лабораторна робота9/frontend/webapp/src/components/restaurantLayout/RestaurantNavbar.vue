<template>
  <h3 class="mt-2">Dishes</h3>
  <ul>
    <dish-type-item
        :item-class="'sidebar-item'"
        :item-chosen-class="'sidebar-item-checked'"
        :id="null"
        :change-category-to-id="0"
        :name="'All'"
        @change-category-to="id => $emit('changeCategoryTo', id)"
    >
    </dish-type-item>
    <dish-type-item
        v-for="dish in dishTypes"
        :key="dish.id"
        :item-class="'sidebar-item'"
        :item-chosen-class="'sidebar-item-checked'"
        :id="dish.id"
        :change-category-to-id="dish.id"
        :name="dish.name"
        @change-category-to="id => $emit('changeCategoryTo', id)"
    >
    </dish-type-item>
  </ul>

  <div class="mt-5">
    <h3>Info</h3>
    <restaurant-links></restaurant-links>
  </div>
</template>

<script>
import RestaurantLinks from "@/components/UI/RestaurantLinks.vue";
import DishTypeItem from "@/components/UI/DishTypeItem.vue";

export default {
  components: {DishTypeItem, RestaurantLinks},
  emits: ['changeCategoryTo'],

  props: {
    dishTypes: Array
  }
}
</script>