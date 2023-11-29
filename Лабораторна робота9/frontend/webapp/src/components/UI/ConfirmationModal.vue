<template>
  <div class="modal fade" id="confirmModal" tabindex="-1" aria-labelledby="confirmModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="modal-title fs-5" id="exampleModalLabel">Confirm</h3>
        </div>
        <div class="modal-body">
          <div class="d-flex align-items-center">
            <span class="flex-grow-1" v-html="getMessage()"></span>
            <button type="button" class="btn btn-secondary me-2"
                    data-bs-dismiss="modal">
              No
            </button>
            <button type="button" class="btn btn-primary"
                    data-bs-dismiss="modal"
                    @click="confirm">
              Yes
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  emits: ['confirmationDelete', 'confirmationComplete'],

  props: {
    itemToConfirm: String,
    modalType: String
  },
  methods: {
    confirm() {
      this.modalType === 'delete'
          ? this.$emit('confirmationDelete')
          : this.$emit('confirmationComplete');
    },
    getMessage() {
      const messages = {
        'delete': `Remove <strong>${this.itemToConfirm}</strong> from cart?`,
        'complete': 'Confirm order?'
      }
      return messages[this.modalType];
    }
  }
}
</script>