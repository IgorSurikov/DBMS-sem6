'use strict';

document.addEventListener('DOMContentLoaded', () => {
    const addForm = document.querySelector('form');
    const message = {
        loading: 'img/form/spinner.svg',
        success: 'all good',
        failure: 'fail...'
    };
    postData(addForm);
    function postData(addForm) {
        addForm.addEventListener('submit', (event) => {
            event.preventDefault();
            let statusMessage = document.createElement('img');
            statusMessage.src = message.loading;
            statusMessage.style.cssText = `
                display: block;
                margin: 0 auto;
            `;
            addForm.insertAdjacentElement('afterend', statusMessage);

            const formData = new FormData(addForm);
            if (formData.get("Password") !== formData.get("repassword")) {
                console.log('No repassword');
                return;
            }
     
            /*const object = {};
            formData.forEach(function (value, key) {
                if (key != "repassword") {
                    object[key] = value;
                }
            });*/
            formData.delete("repassword");
            fetch('account/register', {
                method: 'POST',
                body: formData
            }).then(data => {
                console.log(message.success);
                statusMessage.remove();
            }).catch(() => {
                console.log(message.failure);
            }).finally(() => {
                addForm.reset();
            });

        });
    }


});

