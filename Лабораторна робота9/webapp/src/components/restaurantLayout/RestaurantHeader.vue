<template>
  <header>
    <nav class="navbar bg-light">
      <div :class="['container-fluid', paddingsIfNotMobile()]">
        <a class="btn btn-outline-secondary px-3 py-1"
            @click="$emit('toggleNavbar')">
          <font-awesome-icon :icon="['fas', 'bars']" />
        </a>
        <div class="navbar-brand align-self-end ms-3 me-auto mb-lg-0" href="#"
            v-if="!titleHiden">
          <img src="@/assets/icons/logo.png"
               width="30" height="29"
               alt="logo">
          <span class="fw-semibold">Restaurant</span>
        </div>
        <form class="d-flex" role="search"
            v-if="longSearch">
          <div class="input-group">
            <input class="form-control"
                   type="search"
                   placeholder="Search for dishes"
                   aria-label="Search">
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
      </div>
    </nav>
  </header>
</template>

<script>
  export default {
    data() {
      return {
        input: '',
        titleHiden: true,
        longSearch: true,
        screenWidth: window.innerWidth
      }
    },
    mounted() {
      window.addEventListener('resize', this.updateScreenWidth);
      this.updateScreenWidth();
    },
    methods: {
      search() {
        this.$emit('search', this.input);
      },
      toggleMobileSearch() {
        this.titleHiden = !this.titleHiden;
        this.longSearch = !this.longSearch;
      },
      updateScreenWidth() {
        this.screenWidth = window.innerWidth;
        if (this.screenWidth < 500) {
          this.titleHiden = false;
          this.longSearch = false;
        } else {
          this.titleHiden = false;
          this.longSearch = true;
        }
      },
      paddingsIfNotMobile() {
        if (this.screenWidth > 500) {
          return 'pe-4';
        }
      }
    }
  }
</script>