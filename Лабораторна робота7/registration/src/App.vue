<template>
  <div class="page bg-lightblue">
    <div class="container">
      <div class="col-lg-6 offset-lg-3">
        <registration-form></registration-form>
      </div>
    </div>
    <registrants-table></registrants-table>
  </div>
</template>

<script>
import RegistrationForm from "@/components/RegistrationForm.vue";
import RegistrantsTable from "@/components/RegistrantsTable.vue";

export default {
  components: {RegistrantsTable, RegistrationForm}
}
</script>
