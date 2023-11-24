import Inputmask from "inputmask";

export default {
    methods: {
        formatCurrency(price) {
            return '₴ ' + price.toFixed(2)
                .replace(/\d(?=(\d{3})+\.)/g, '$&');
        },
        validateForm() {
            const form = this.$refs.regForm;
            if (form.checkValidity()) {
                return true
            } else {
                form.classList.add('was-validated');
            }
        },
        addPhoneMask(phone) {
            const inputMask = new Inputmask('+380 (99) 999-99-99');
            inputMask.mask(phone);
        },
        restrictDateInputWithYesterdayDate(dateInput) {
            const date = new Date();
            date.setDate(date.getDate() - 1);

            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            dateInput.max = year + '-' + month + '-' + day;
        },
        buyAnimation() {
            const cart = document.getElementById('cart');
            cart.classList.add('shake-animation');
            setTimeout(() => {
                cart.classList.remove("shake-animation");
            }, 500)
        },
        alertDanger(message) {
            this.$parent.$refs.dismissibleAlert.alert('alert-danger', message);
        },
        alertWarning(message) {
            this.$parent.$refs.dismissibleAlert.alert('alert-warning', message);
        },
        alertSuccess(message) {
            this.$parent.$refs.dismissibleAlert.alert('alert-success', message);
        }
    }
}