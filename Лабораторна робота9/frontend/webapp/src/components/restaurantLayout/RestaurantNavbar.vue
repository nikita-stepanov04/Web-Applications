<template>
  <h3 class="mt-2">Dishes</h3>
  <ul>
    <li v-for="dishType in dishTypes"
        :key="dishType">
      <div :class="{
          'sidebar-item': true,
          'sidebar-item-checked': dishType.checked
        }"
      >
        <a class="main-link" href="#"
            @click="$emit('changeCategoryTo', dishType.name)">
          {{dishType.name}}
        </a>
      </div>
    </li>
  </ul>

  <div class="mt-5">
    <h3>Info</h3>
    <restaurant-links></restaurant-links>
  </div>
</template>

<script>
import RestaurantLinks from "@/components/UI/RestaurantLinks.vue";

export default {
  components: {RestaurantLinks},
  emits: ['changeCategoryTo'],
  props: {
    dishTypes: Array
  }
}
</script>