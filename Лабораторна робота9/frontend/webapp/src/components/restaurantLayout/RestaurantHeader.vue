<template>
  <header>
    <nav class="navbar bg-light">
      <div :class="{
          'container-fluid': true,
          'px-4': screenWidth > 500
        }"
      >

<!--        toggle navbar button-->

        <a class="btn btn-outline-secondary px-3 py-1"
            @click="toggleNavbar"
            data-bs-toggle="collapse"
            data-bs-target="#navbarHidingContent"
            ref="toggleNavbar"
           >
          <font-awesome-icon :icon="['fas', 'bars']" />
        </a>

<!--        restaurant logo with name-->

        <a class="navbar-brand align-self-end ms-3 me-auto mb-lg-0" href="#"
           v-if="!titleHiden">
          <img src="@/assets/icons/logo.png"
               width="30" height="29"
               alt="logo">
          <span class="fw-semibold">Restaurant</span>
        </a>

<!--        cart button-->

        <a class="navbar-brand btn btn-outline-secondary px-2 py-1"
            v-if="!titleHiden"
            href="/cart">
          <span class="align-self-end me-auto mb-lg-0 position-relative">
            <font-awesome-icon :icon="['fas', 'cart-shopping']"/>
            <span class="position-absolute start-100
                translate-middle badge rounded-pill
                bg-danger fw-light py-1 px-2 ms-1">
              {{itemsInCart > 9 ? '9+' : itemsInCart}}
            </span>
          </span>
        </a>

<!--        search form-->

        <form class="d-flex" role="search"
              v-if="longSearch"
              @submit.prevent="search">
          <div class="input-group">
            <input class="form-control"
                 type="search"
                 placeholder="Search for dishes"
                 aria-label="Search"
                 v-model="input">
            <button class="btn btn-outline-success" type="submit">
              <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
            </button>
          </div>
          <button class="btn btn-outline-secondary ms-1 px-1"
                  v-if="titleHiden"
                  @click="toggleMobileSearch">
            <font-awesome-icon :icon="['fas', 'circle-xmark']" />
          </button>
        </form>
        <button class="btn"
            v-else
            @click="toggleMobileSearch">
          <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
        </button>

<!--        on mobile collapsing navbar-->

        <div class="collapse navbar-collapse"
              id="navbarHidingContent"
              ref="navbarHidingContent"
              v-if="screenWidth <= 800">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0 mt-2">
            <li class="nav-item dropdown ms-1">
              <a class="btn dropdown-toggle fw-semibold" href="#" role="button"
                 data-bs-toggle="dropdown" aria-expanded="false">
                Dishes
              </a>
              <ul class="dropdown-menu">
                <li v-for="(dishType, key) in dishTypes" :key="key">
                  <a :class="{
                        'dropdown-item': true,
                        'link-primary': dishType.checked
                      }"
                     href="#"
                     @click="$emit('changeCategoryTo', dishType.name)"
                  >
                    {{dishType.name}}
                  </a>
                </li>
              </ul>
            </li>
          </ul>
          <restaurant-links class="sidebar">
          </restaurant-links>
        </div>
      </div>
    </nav>
  </header>
</template>

<script>
  import RestaurantLinks from "@/components/UI/RestaurantLinks.vue";

  export default {
    components: {RestaurantLinks},
    emits: ['changeCategoryTo', 'toggleNavbar', 'search'],
    data() {
      return {
        input: '',
        titleHiden: true,
        longSearch: true,
        screenWidth: window.innerWidth
      }
    },
    props: {
      dishTypes: Array,
      itemsInCart: Number,
    },
    mounted() {
      window.addEventListener('resize', this.updateScreenWidth);
      this.updateScreenWidth();
    },
    methods: {
      search() {
        this.$emit('search', this.input);
        this.input = '';
      },
      toggleMobileSearch() {
        this.titleHiden = !this.titleHiden;
        this.longSearch = !this.longSearch;
      },
      updateScreenWidth() {
        this.screenWidth = window.innerWidth;
        this.titleHiden = false;
        this.longSearch = this.screenWidth > 600;

        this.screenWidth > 800
            ? this.$refs.toggleNavbar?.removeAttribute('data-bs-toggle')
            : this.$refs.toggleNavbar?.setAttribute('data-bs-toggle', 'collapse');
      },
      toggleNavbar() {
        if (this.screenWidth > 800) {
          this.$emit('toggleNavbar');
        }
      },
    }
  }
</script>