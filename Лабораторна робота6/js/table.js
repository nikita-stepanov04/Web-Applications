form.addEventListener('submit', event => {
    event.preventDefault();

    // get the index of the last row in the table
    let index = 0;
    if (document.querySelector('#registrants tbody').lastChild != null) {
        const previousIndex = document.querySelector('#registrants tbody')
            .lastChild.querySelector('td')
            .firstChild.textContent;
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