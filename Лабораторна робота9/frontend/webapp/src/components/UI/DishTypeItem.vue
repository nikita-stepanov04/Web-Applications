<template>
  <li>
    <div :class="{
          [itemClass]: true,
          [itemChosenClass]: $store.state.chosenDishTypeId === id
        }"
        @click="$emit('changeCategoryTo', changeCategoryToId)"
    >
      <a class="main-link" href="#">
        {{name}}
      </a>
    </div>
  </li>
</template>

<script>
export default {
  emits: ['changeCategoryTo'],
  props: {
    itemClass: String,
    itemChosenClass: String,
    id: Number,
    changeCategoryToId: Number,
    name: String
  }
}
</script>