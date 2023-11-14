<template>
  <li>
    <div :class="{
          [itemClass]: true,
          [itemChosenClass]: $store.state.chosenDishTypeId === id
        }"
    >
      <a class="main-link"
         @click="$emit('changeCategoryTo', changeCategoryToId)">
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