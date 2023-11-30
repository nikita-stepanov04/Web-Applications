<template>
  <dismissible-alert ref="dismissibleAlert"></dismissible-alert>

  <nav class="navbar navbar-expand-md bg-body-tertiary">
    <div class="container-fluid">
      <a class="navbar-brand align-self-end me-auto mb-lg-0" href="#">
        <img src="../../../public/logo.png"
             width="30" height="29"
             alt="logo">
        <span class="fw-semibold me-4">Restaurant Administration</span>
      </a>
      <button class="navbar-toggler"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarSupportedContent"
              aria-controls="navbarSupportedContent"
              aria-expanded="false"
              aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link to="/admin/orders"
               class="nav-link active">Orders</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/admin/dishes"
               class="nav-link active">Dishes</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/admin/dish-types/edit"
               class="nav-link active">DishTypes</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/admin/schedule/edit"
               class="nav-link active">Schedule</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/admin/stats"
               class="nav-link active">Stats</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/menu"
               class="nav-link active">Menu</router-link>
          </li>

        </ul>
      </div>
    </div>
  </nav>
  <div class="container-fluid mt-3">
    <div class="row">
      <div class="col-12">
        <div class="table-responsive px-3 py-2">
          <slot></slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DismissibleAlert from "@/components/UI/DismissibleAlert.vue";

export default {
  components: {DismissibleAlert}
}
</script>