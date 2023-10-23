<template>
  <div v-if="$store.state.registrantsTableVisibility" class="registrants px-2" ref="registrantsTable">
    <div class="container-fluid bg-light rounded-4 mb-5">
      <div class="table-responsive p-2">
        <table class="table table-striped" id="registrants">
          <thead>
          <tr>
            <th></th>
            <th>#</th>
            <th>Name</th>
            <th>Surname</th>
            <th>Email</th>
            <th>Password</th>
            <th>Birthday</th>
            <th>Group</th>
            <th>Phone</th>
            <th>Gender</th>
            <th>Avatar</th>
          </tr>
          </thead>
          <tbody>
            <tr v-for="(registrant, index) in $store.state.registrants" :key="registrant.id">
              <td><input type="checkbox" v-model="registrant.checked" @change="disableTableButtonsIfNoCheckedInputs"></td>
              <td>{{index + 1}}</td>
              <td>{{registrant.name}}</td>
              <td>{{registrant.surname}}</td>
              <td>{{registrant.email}}</td>
              <td>{{registrant.password}}</td>
              <td>{{registrant.birthday}}</td>
              <td>{{registrant.group}}</td>
              <td>{{registrant.phone}}</td>
              <td>{{registrant.gender}}</td>
              <td>{{registrant.avatar}}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="row my-2 pb-3">
        <div class="col-3 offset-3">
          <div class="d-grid">
            <button class="btn btn-primary" :disabled="copyButtonDisabled"
                @click="copyButtonClick">
              Clone
            </button>
          </div>
        </div>
        <div class="col-3">
          <div class="d-grid">
            <button class="btn btn-danger" :disabled="deleteButtonDisabled"
                @click="deleteButtonClick">
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      copyButtonDisabled: true,
      deleteButtonDisabled: true
    }
  },
  methods: {
    disableTableButtonsIfNoCheckedInputs() {
      this.disableButtons(true);
      for(const reg of this.$store.state.registrants) {
        if (reg.checked) {
          this.disableButtons(false);
          break;
        }
      }
    },
    disableButtons(exp) {
      this.copyButtonDisabled = exp;
      this.deleteButtonDisabled = exp;
    },
    copyButtonClick() {
      this.$store.commit('copyCheckedRegistrants');
      this.disableTableButtonsIfNoCheckedInputs();
    },
    deleteButtonClick() {
      this.$store.commit('removeCheckedRegistrants');
      this.disableTableButtonsIfNoCheckedInputs();
    }
  }
}
</script>