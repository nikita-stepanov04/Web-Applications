<template>
  <tr class="align-middle">
    <td>{{cartLine.name}}</td>
    <td>{{cartLine.price}}</td>
    <td class="on-mobile-cart-column-hidden">
      {{cartLine.price * cartLine.quantity}}
    </td>
    <td class="text-end">
      <button class="btn btn-outline-secondary px-1 py-0"
          @click="$store.commit('increaseDishQuantity', {id: cartLine.dishId, increaseBy: -1})">
        <font-awesome-icon :icon="['fas', 'minus']" />
      </button>
      <span class="mx-2 align-middle">{{cartLine.quantity}}</span>
      <button class="btn btn-outline-secondary px-1 py-0"
          @click="$store.commit('increaseDishQuantity', {id: cartLine.dishId, increaseBy: 1})">
        <font-awesome-icon :icon="['fas', 'plus']" />
      </button>
    </td>
    <td class="text-end">
      <button class="btn btn-primary px-2 py-1"
          data-bs-toggle="modal" data-bs-target="#confirmModal"
          @click="$emit('confirmRemove', {id: cartLine.dishId, name: cartLine.name})">
        <font-awesome-icon :icon="['fas', 'trash']" />
      </button>
    </td>
  </tr>
</template>

<script>
export default {
  emits: ['confirmRemove'],

  props: {
    cartLine: Object
  }
}
</script>