<template>
  <transition>
    <div ref="liveAlertPlaceholder" class="live-alert-container" v-if="showAlert">
      <div class="container">
        <div class="row">
          <div class="col-6 offset-3 mt-3">
            <div :class="['alert', alertType, 'alert-dismissible']" role="alert">
              <div>{{message}}</div>
              <button
                  class="btn-close"
                  aria-label="Close"
                  @click="this.showAlert=false"
              >
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
  export default {
    data() {
      return {
        showAlert: false,
        alertType: '',
        message: ''
      }
    },
    methods: {
      alert(alertType, message) {
        this.showAlert = true;
        this.alertType = alertType;
        this.message = message;
        setTimeout(() => this.showAlert = false, 2000);
      }
    }
  }
</script>

<style scoped>
.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease, transform 0.5s ease;
  transform: scale(1);
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
  transform: scale(0.8);
}
.live-alert-container {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 9999;
}
</style>
