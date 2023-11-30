<template>
  <div class="container">
    <form @submit.prevent="submitChange"
          ref="regForm"
          class="needs-validation"
          id="register-form"
          novalidate>
      <div class="row justify-content-center">

       <div class="col-lg-6 col-md-8">
         <h3 class="text-center mb-5">Restaurant Schedule</h3>
         <table class="table table-striped table-light mt-3">
           <thead>
           <tr>
             <th>Day of the Week</th>
             <th>Opening Hours</th>
           </tr>
           </thead>
           <tbody>
           <tr
               v-for="(time, day) in schedule"
               :key="day"
           >
             <td class="text-capitalize" v-if="day !== 'id'">{{day}}</td>
             <td v-if="day !== 'id'">
               <input class="form-control" v-model="schedule[day]"
                      pattern="\d{1,2}:\d{2} - \d{1,2}:\d{2}$"
                      required>
               <div class="invalid-feedback text-center">
                 Please enter working time (hh:mm - hh:mm)
               </div>
             </td>
           </tr>
           </tbody>
         </table>
         <div class="d-grid mt-3">
           <button class="btn btn-primary" type="submit">
             Submit changes
           </button>
         </div>
       </div>
      </div>
    </form>
  </div>
</template>

<script>
import request from "@/api/requestConstructor";
import helpers from "@/mixins/helpers";
import {compare} from "fast-json-patch";

export default {
  mixins: [helpers, request],

  data() {
    return {
      schedule: {},
      scheduleCopy: null
    }
  },
  methods: {
    async submitChange() {
      if (this.validateForm()) {
        if (JSON.stringify(this.schedule) !== (JSON.stringify(this.scheduleCopy))) {
          const patch = compare(this.scheduleCopy, this.schedule);
          try {
            await request.patch('schedule', patch, true)
            this.alertSuccess('Successfully patched schedule')
            this.scheduleCopy = JSON.parse(JSON.stringify(this.schedule))
          } catch (error) {
            this.alertDanger('Failed to patch schedule')
          }

        } else {
          this.alertWarning('Nothing to save')
        }
      }
    }
  },
  async created() {
    try {
      this.schedule = (await request.get('schedule')).data;
      this.scheduleCopy = JSON.parse(JSON.stringify(this.schedule));
    } catch (error) {
      this.$refs.dismissibleAlert.alert(
          'alert-danger', 'Failed to load schedule')
    }
  }
}
</script>