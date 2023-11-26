<template>
  <blue-gradient-layout>
    <dismissible-alert ref="dismissibleAlert"></dismissible-alert>
    <h3>Restaurant Schedule</h3>
    <table class="table table-striped table-light mt-3">
      <thead>
      <tr>
        <th>Day of the Week</th>
        <th>Opening Hours</th>
      </tr>
      </thead>
      <tbody>
        <tr
            v-for="[day, time] in Object.entries(schedule)"
            :key="day"
        >
          <td class="text-capitalize" v-if="day !== 'id'">{{day}}</td>
          <td v-if="day !== 'id'">{{time}}</td>
        </tr>
      </tbody>
    </table>
    <div class="d-grid mt-3">
      <router-link
          class="btn btn-primary"
          to="/menu"
      >
        To menu
      </router-link>
    </div>
  </blue-gradient-layout>
</template>
<script>
import BlueGradientLayout from "@/components/blueGradientLayout/BlueGradientLayout.vue";
import DismissibleAlert from "@/components/UI/DismissibleAlert.vue";
import request from "@/api/requestConstructor"

export default {
  components: {BlueGradientLayout, DismissibleAlert},

  data() {
    return {
      schedule: {}
    }
  },
  async created() {
    try {
      this.schedule = (await request.get('schedule')).data;
    } catch (error) {
      this.$refs.dismissibleAlert.alert(
          'alert-danger', 'Failed to load schedule')
    }
  }
}
</script>