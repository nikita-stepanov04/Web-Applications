import Inputmask from "inputmask";

export default {
    methods: {
        formatCurrency(price) {
            return '₴ ' + price.toFixed(2)
                .replace(/\d(?=(\d{3})+\.)/g, '$&');
        },
        formatDate(date) {
            const dateObject = new Date(date);
            const options = {
                day: "2-digit",
                month: "2-digit",
                year: "numeric",
                hour: "2-digit",
                minute: "2-digit",
                hour12: false,
            };
            return new Intl.DateTimeFormat("ru-Ru", options).format(dateObject);
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
        },
        colorByNumber(n) {
            const colors = [
                '#2165ff',
                '#2ecc71',
                '#ff1700',
                '#eec128',
                '#1cd4ff',
                '#ff841f',
                '#c81cff'
            ]
            return colors[n % 7];
        }
    }
}