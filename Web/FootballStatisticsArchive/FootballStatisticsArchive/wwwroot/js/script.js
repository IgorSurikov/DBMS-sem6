'use strict';

document.addEventListener('DOMContentLoaded', async() => {
    const modalTrigger = document.querySelector('[data-modal]'),
        modalTriggerEnter = document.querySelector('[data-modalenter]'),
        modal = document.querySelector('.modal'),
        modalEnter = document.querySelector('.modal__enter');

    const postData = async (url, data) => {
        let res = await fetch(url, {
            method: "POST",
            body: data
        });
    
        return await res;
    };

    async function getResource(url) {
        let res = await fetch(url);
    
        if (!res.ok) {
            throw new Error(`Could not fetch ${url}, status: ${res.status}`);
        }
    
        return await res.json();
    }
    //Tour
    const rel = await getResource('tournament')
    .then(data => {
        data.forEach(({country, fourth, goalsScored, matchesPlayed, name, qualifiedTeams, runnersUp, Third, TournamentId, Winner, Year}) => {
            console.log(country);
            console.log(fourth);
            console.log(goalsScored);
            console.log(matchesPlayed);
            console.log(name);
        });
    });
    //Enter
    function closeModalEnter() {
        modalEnter.classList.add('hide');
        modalEnter.classList.remove('show');
        document.body.style.overflow = '';
    }

    function openModalEnter() {
        modalEnter.classList.add('show');
        modalEnter.classList.remove('hide');
        document.body.style.overflow = 'hidden';
        clearInterval(modalTimerId);
    }

    modalEnter.addEventListener('click', (e) => {
        if (e.target === modalEnter || e.target.getAttribute('data-close') == "") {
            closeModalEnter();
        }
    });

    document.addEventListener('keydown', (e) => {
        if (e.code === "Escape" && modalEnter.classList.contains('show')) { 
            closeModalEnter();
        }
    });
    const messageEnter = {
        loading: 'img/form/spinner.svg',
        success: 'Вы вошли в аккаунт',
        failure: 'Что-то пошло не так...'
    };
    const modalTimerId = setTimeout(openModalEnter, 3000);    
    const formCheck = document.querySelector('.form');
    check(formCheck);
    function check(formCheck) {
        formCheck.addEventListener('submit', async(e) => {
            e.preventDefault();

            let statusMessage = document.createElement('img');
            statusMessage.src = message.loading;
            statusMessage.style.cssText = `
                display: block;
                margin: 0 auto;
            `;
            formCheck.insertAdjacentElement('afterend', statusMessage);
        
            const formData = new FormData(formCheck);
            
            postData('account/login', formData)
            .then(data => {
                if (data.status == 200) {
                    showThanksModal(messageEnter.success);
                    statusMessage.remove();
                    closeModalEnter();
                    modalTrigger.classList.add('hide');
                    modalTriggerEnter.classList.add('hide');

                }
                else
                {
                    throw `error with status ${data.status}`;
                }
            }).catch(() => {
                showThanksModal(messageEnter.failure);
            }).finally(() => {
                formCheck.reset();
            });

        });
    }

    //Registration
    
  
    function closeModal() {
        modal.classList.add('hide');
        modal.classList.remove('show');
        document.body.style.overflow = '';
    }

    function openModal() {
        modal.classList.add('show');
        modal.classList.remove('hide');
        document.body.style.overflow = 'hidden';
        clearInterval(modalTimerId);
    }

    modal.addEventListener('click', (e) => {
        if (e.target === modal || e.target.getAttribute('data-close') == "") {
            closeModal();
        }
    });

    document.addEventListener('keydown', (e) => {
        if (e.code === "Escape" && modal.classList.contains('show')) { 
            closeModal();
        }
    });
    const form = document.querySelector('.form_reg');
    const message = {
        loading: 'img/form/spinner.svg',
        success: 'Спасибо! Скоро мы с вами свяжемся',
        failure: 'Что-то пошло не так...'
    };
    modalTrigger.addEventListener('click', openModal);
    modalTriggerEnter.addEventListener('click', openModalEnter);

    bindPostData(form);

    function bindPostData(form) {
        form.addEventListener('submit', (e) => {
            e.preventDefault();

            let statusMessage = document.createElement('img');
            statusMessage.src = message.loading;
            statusMessage.style.cssText = `
                display: block;
                margin: 0 auto;
            `;
            form.insertAdjacentElement('afterend', statusMessage);
        
            const formData = new FormData(form);
            if (formData.get("Password") !== formData.get("repassword")) {
                console.log('No repassword');
                return;
            }
            formData.delete("repassword");
            postData('account/register', formData)
            .then(data => {
                if (data.status == 200) {
                    showThanksModal(message.success);
                    statusMessage.remove();
                    closeModal();
                }
                else
                {
                    throw `error with status ${data.status}`;
                }
            }).catch(() => {
                showThanksModal(message.failure);
            }).finally(() => {
                form.reset();
            });
        });
    }

    function showThanksModal(message) {
        const prevModalDialog = document.querySelector('.modal__dialog');

        prevModalDialog.classList.add('hide');
        openModal();

        const thanksModal = document.createElement('div');
        thanksModal.classList.add('modal__dialog');
        thanksModal.innerHTML = `
            <div class="modal__content">
                <div class="modal__close" data-close>×</div>
                <div class="modal__title">${message}</div>
            </div>
        `;
        document.querySelector('.modal').append(thanksModal);
        setTimeout(() => {
            thanksModal.remove();
            prevModalDialog.classList.add('show');
            prevModalDialog.classList.remove('hide');
            closeModal();
        }, 4000);
    }

    //Tour


    // const addForm = document.querySelector('form');
    // const message = {
    //     loading: 'img/form/spinner.svg',
    //     success: 'all good',
    //     failure: 'fail...'
    // };
    // postData(addForm);
    // function postData(addForm) {
    //     addForm.addEventListener('submit', (event) => {
    //         event.preventDefault();
    //         let statusMessage = document.createElement('img');
    //         statusMessage.src = message.loading;
    //         statusMessage.style.cssText = `
    //             display: block;
    //             margin: 0 auto;
    //         `;
    //         addForm.insertAdjacentElement('afterend', statusMessage);

    //         const formData = new FormData(addForm);
    //         if (formData.get("Password") !== formData.get("repassword")) {
    //             console.log('No repassword');
    //             return;
    //         }
     
    //         const object = {};
    //         formData.forEach(function (value, key) {
    //             if (key != "repassword") {
    //                 object[key] = value;
    //             }
    //         });
    //         formData.delete("repassword");
    //         fetch('account/register', {
    //             method: 'POST',
    //             body: formData
    //         }).then(data => {
    //             console.log(message.success);
    //             statusMessage.remove();
    //         }).catch(() => {
    //             console.log(message.failure);
    //         }).finally(() => {
    //             addForm.reset();
    //         });

    //     });
    // }


});

