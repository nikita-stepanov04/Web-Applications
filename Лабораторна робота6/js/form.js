const tels = document.querySelectorAll('#phone');
const inputMask = new Inputmask('+380 (99) 999-99-99');
inputMask.mask(tels);

const form = document.querySelector('.needs-validation');
form.addEventListener('submit', event => {
    if (!form.checkValidity()) {
        event.preventDefault();
        event.stopPropagation();
    }
    form.classList.add('was-validated');
}, true);