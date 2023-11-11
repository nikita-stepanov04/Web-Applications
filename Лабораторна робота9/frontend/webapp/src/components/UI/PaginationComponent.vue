<template>
  <div class="d-flex justify-content-center mt-4"
      v-if="paginationInfo.totalPages > 1">
    <nav aria-label="...">
      <ul class="pagination pagination-md">
        <li v-for="pageNumber in paginationInfo.totalPages"
            :key="pageNumber"
            :class="{
              'page-item': true,
              'active': pageNumber === paginationInfo.pageNumber
            }">
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
    paginationInfo: Object
  }
}
</script>