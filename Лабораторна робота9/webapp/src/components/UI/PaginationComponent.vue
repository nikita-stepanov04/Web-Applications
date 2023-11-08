<template>
  <div class="d-flex justify-content-center mt-4">
    <nav aria-label="...">
      <ul class="pagination pagination-md">
        <li v-for="pageNumber in totalPages"
            :key="pageNumber"
            :class="getPaginationClass(pageNumber)">
          <a class="page-link"
             @click="$emit('change-page', pageNumber)">
            {{pageNumber}}
          </a>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
export default {
  emits: ['change-page'],
  props: {
    totalPages: Number,
    currantPage: Number
  },
  methods: {
    getPaginationClass(pageNumber) {
      return pageNumber === this.currantPage
          ? 'page-item active'
          : 'page-item'
    }
  }
}
</script>