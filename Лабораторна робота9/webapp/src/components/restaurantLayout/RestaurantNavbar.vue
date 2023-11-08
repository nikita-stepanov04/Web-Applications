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
    <ul>
      <li>
        <div class="sidebar-item">
          <a class="main-link" href="#">
            <font-awesome-icon :icon="['fas', 'user']"
              class="me-2"/>
            My account
          </a>
        </div>
      </li>
      <li>
        <div class="sidebar-item">
          <a class="main-link" href="#">
            <font-awesome-icon :icon="['fas', 'cart-shopping']"
              class="me-2"/>
            My orders
          </a>
        </div>
      </li>
      <li>
        <div class="sidebar-item">
          <a class="main-link" href="#">
            <font-awesome-icon :icon="['fas', 'address-book']"
              class="me-2"/>
            Contacts
          </a>
        </div>
      </li>
      <li>
        <div class="sidebar-item">
          <a class="main-link" href="#">
            <font-awesome-icon :icon="['fas', 'circle-info']"
              class="me-2"/>
            FAQ
          </a>
        </div>
      </li>
      <li>
        <div class="sidebar-item">
          <a class="main-link" href="#">
            <font-awesome-icon :icon="['fas', 'calendar-days']"
              class="me-2"/>
            Schedule
          </a>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  emits: ['changeCategoryTo'],
  props: {
    dishTypes: Array
  }
}
</script>