<template>
  <ul>
    <li>
      <div class="sidebar-item">
        <a class="main-link" href="#">
          <font-awesome-icon :icon="['fas', 'user']"/>
          My account
        </a>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <a class="main-link" href="/menu">
          <font-awesome-icon :icon="['fas', 'bowl-food']" />
          Dishes
        </a>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <a class="main-link" href="/cart">
          <font-awesome-icon :icon="['fas', 'cart-shopping']"/>
          My orders
        </a>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <a class="main-link" href="#">
          <font-awesome-icon :icon="['fas', 'address-book']"/>
          Contacts
        </a>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <a class="main-link" href="#">
          <font-awesome-icon :icon="['fas', 'circle-info']"/>
          FAQ
        </a>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <a class="main-link" href="#">
          <font-awesome-icon :icon="['fas', 'calendar-days']"/>
          Schedule
        </a>
      </div>
    </li>
  </ul>
</template>
<script setup>
</script>