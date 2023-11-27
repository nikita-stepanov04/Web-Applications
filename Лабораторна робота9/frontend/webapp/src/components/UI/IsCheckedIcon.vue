<template>
  <font-awesome-icon :icon="
    condition
      ? ['far', 'circle-check']
      : ['far', 'circle-xmark']"
    :class="condition ? 'text-success' : 'text-danger'"/>
</template>

<script>
export default {
  props: {
    condition: Boolean,
  }
}
</script>