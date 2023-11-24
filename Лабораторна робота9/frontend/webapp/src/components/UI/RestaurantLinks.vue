<template>
  <ul>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/my-account">
          <font-awesome-icon :icon="['fas', 'user']"/>
          My account
        </router-link>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/menu">
          <font-awesome-icon :icon="['fas', 'bowl-food']" />
          Dishes
        </router-link>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/cart">
          <font-awesome-icon :icon="['fas', 'cart-shopping']"/>
          Cart
        </router-link>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/">
          <font-awesome-icon :icon="['fas', 'address-book']"/>
          Contacts
        </router-link>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/">
          <font-awesome-icon :icon="['fas', 'circle-info']"/>
          FAQ
        </router-link>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/">
          <font-awesome-icon :icon="['fas', 'calendar-days']"/>
          Schedule
        </router-link>
      </div>
    </li>
  </ul>
</template>
<script setup>
</script>