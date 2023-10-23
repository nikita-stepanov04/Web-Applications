//mask for phones
const tels = document.querySelectorAll('#phone');
const inputMask = new Inputmask('+380 (99) 999-99-99');
inputMask.mask(tels);

//restrict date in form
const yesterday = new Date();
yesterday.setDate(yesterday.getDate() - 1);

const year = yesterday.getFullYear();
const month = String(yesterday.getMonth() + 1).padStart(2, '0');
const day = String(yesterday.getDate()).padStart(2, '0');
const formattedDate = year + '-' + month + '-' + day;
console.log(formattedDate)
document.querySelector('#birthday').setAttribute('max', formattedDate);

//validation form
// const form = document.querySelector('.needs-validation');
// form.addEventListener('submit', event => {
//     if (!form.checkValidity()) {
//         event.preventDefault();
//         event.stopPropagation();
//     }
//     form.classList.add('was-validated');
// }, true);