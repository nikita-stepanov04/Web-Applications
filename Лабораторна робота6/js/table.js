const tbody = document.querySelector('#registrants tbody');

form.addEventListener('submit', event => {
    event.preventDefault();

    // get the index of the last row in the table
    let index = 0;
    if (tbody.lastChild != null) {
        const previousIndex = tbody.lastChild.children[1].textContent;
        index = parseInt(previousIndex) + 1;
    }

    const input = [];
    input.push(index);
    input.push(document.querySelector('#name').value);
    input.push(document.querySelector('#surname').value);
    input.push(document.querySelector('#email').value);
    input.push(document.querySelector('#password').value);
    input.push(document.querySelector('#birthday').value);
    input.push(document.querySelector('#group').value);
    input.push(document.querySelector('#phone').value);
    input.push(document.querySelector('input[name="gender"]:checked').value);
    // get the file name from the path and push it
    const avatarPath = document.querySelector('#avatar').value.split('\\');
    input.push(avatarPath[avatarPath.length - 1]);

    const tr = document.createElement('tr');

    const tdWithCheckbox = document.createElement('td');
    const checkbox = document.createElement('input');
    checkbox.classList.add('form-check-input');
    checkbox.setAttribute('class', 'form-check-input');
    checkbox.setAttribute('type', 'checkbox');
    checkbox.setAttribute('id', index.toString());
    tdWithCheckbox.append(checkbox);
    tr.append(tdWithCheckbox);

    input.forEach(element => {
        const td = document.createElement('td');
        td.append(element);
        tr.append(td);
    });

    document.querySelector('#registrants tbody').appendChild(tr);
    document.querySelector('#registrants tbody').appendChild(tr);
    document.querySelector('.registrants').classList.remove('d-none');

    form.reset();
    form.classList.remove('was-validated');
}, false)

function cloneRegistrant() {
    tbody.querySelectorAll('tr')
        .forEach(tr => {
            const input = tr.querySelector('td input')
            if (input.checked) {
                input.checked = false;
                const clonedTr = tr.cloneNode(true);
                tbody.insertBefore(clonedTr, tr);
            }
        });
    redefineIndexes();
}

function deleteRegistrant() {
    tbody.querySelectorAll('tr')
        .forEach(tr => {
            const input = tr.querySelector('td input')
            if (input.checked) {
                tbody.removeChild(tr);
            }
        });
    redefineIndexes();
}

function redefineIndexes() {
    tbody.querySelectorAll('tr')
        .forEach((tr, index) => {
            tr.querySelector('td input').id = index.toString();
            tr.children[1].textContent = index.toString();
        })
}

