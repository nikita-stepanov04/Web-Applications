document.querySelectorAll(".user")
    .forEach(form => form.addEventListener('submit', (event) => {
        event.preventDefault();  
              
        const chat = document.querySelector('.chat-container');

        const messageRow = document.createElement('div');
        messageRow.classList.add('row');

        const messageDiv = document.createElement('div');
        messageDiv.classList.add("col-8", "message");
        if (event.target.id == 'user2') {
            messageDiv.classList.add('offset-4');
            messageDiv.classList.add('text-end');
        }

        const header = document.createElement('h5');
        header.innerHTML = event.target.id.toUpperCase();

        const text = document.createElement('p');
        const input =  event.target.querySelector('input')
        text.innerHTML = input.value;
        input.value = '';

        messageDiv.appendChild(header);
        messageDiv.appendChild(text);
        messageRow.appendChild(messageDiv);
        chat.append(messageRow);
    }));