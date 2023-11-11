<template>
  <tr class="align-middle">
    <td>{{cartLine.quantity}}</td>
    <td>{{cartLine.name}}</td>
    <td>{{cartLine.price}}</td>
    <td>{{cartLine.price * cartLine.quantity}}</td>
    <td class="text-end">
      <button class="btn btn-primary px-2 py-1">
        <font-awesome-icon :icon="['fas', 'trash']" />
      </button>
    </td>
  </tr>
</template>

<script>
export default {
  props: {
    cartLine: Object
  }
}
</script>