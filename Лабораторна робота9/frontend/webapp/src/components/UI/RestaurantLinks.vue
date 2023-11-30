<template>
  <ul>
    <li v-if="admin">
      <div class="sidebar-item">
        <router-link class="main-link" to="/admin">
          <font-awesome-icon :icon="['fas', 'user-shield']" />
          Administration
        </router-link>
      </div>
    </li>
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
        <router-link class="main-link" to="/orders">
          <font-awesome-icon :icon="['fas', 'truck']" />
          My orders
        </router-link>
      </div>
    </li>
    <li>
      <div class="sidebar-item">
        <router-link class="main-link" to="/schedule">
          <font-awesome-icon :icon="['fas', 'calendar-days']"/>
          Schedule
        </router-link>
      </div>
    </li>
  </ul>
</template>

<script>

export default {
  data() {
    return {
      admin: false
    }
  },
  async created() {
    this.admin = this.$store.state.userAuthenticationRole
        ?? await this.$store.dispatch('checkUserAuthorization') === 'Admin';
  }
}
</script>