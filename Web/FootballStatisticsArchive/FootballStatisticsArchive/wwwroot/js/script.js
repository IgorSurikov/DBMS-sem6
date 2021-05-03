'use strict';

document.addEventListener('DOMContentLoaded', async () => {
    const modalTrigger = document.querySelector('[data-modal]'),
        modalTriggerEnter = document.querySelector('[data-modalenter]'),
        modalTriggerExit = document.querySelector('[data-modalexit]'),
        modalTriggerUsers = document.querySelector('[data-modalusers]'),
        modal = document.querySelector('.modal'),
        modalEnter = document.querySelector('.modal__enter'),
        selectMain = document.querySelector(".select-css"),
        sportHeader = document.querySelector(".sport-header");

    let tour = null,
        teamName = null,
        i = 1;

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
    //Users
    class Users {
        constructor(id, email, nickname, userRole, parentSelector) {
            this.id = id;
            this.email = email;
            this.nickname = nickname;
            this.userRole = userRole;
            this.parentSelector = parentSelector;
            this.parent = document.querySelector(parentSelector);
        }
        render() {
            //document.querySelector(".tabel-match").classList.add("hide");
            document.querySelector(".tabel-match").innerHTML = "";
            document.querySelector(".team-match").innerHTML = "";
            const element = document.createElement('tr');
            
            // teamName.forEach(item => {
            //     item.classList.add('hide');
            // });
         
            element.innerHTML = `
            <td width="20%">${this.email}</td>
            <td width="15%">${this.nickname}</td>
            <td width="20%">
                    <input class="input-ch ${this.id}" type="text"  name="Role" placeholder="${this.userRole.name}"/>
                    <button class="btn-ch ${this.id}" type="submit">Изменить</button>
            </td>
            <td width="5%">
                <button class="btn-delete ${this.id} ${this.userRole.id}">Удалить</button>
            </td>
            `;
            this.parent.append(element);
          
              
        }
    }
    //Team
    class Team {
        constructor(teamId, name, initial, players, parentSelector) {
            this.teamId = teamId;
            this.name = name;
            this.initial = initial;
            this.players = players;
            this.parentSelector = parentSelector;
            this.parent = document.querySelector(parentSelector);
        }
        render() {
            document.querySelector(".tabel-match").classList.add("hide");
            
            const element = document.createElement('tr');
           
            // teamName.forEach(item => {
            //     item.classList.add('hide');
            // });
         
            element.innerHTML = `
            <td width="21%">${this.name}</td>
            <td width="14%">${this.initial}</td>
            `;
            this.parent.append(element);
            this.players.forEach(({name, shirtNumber}) => {
                const elementNew = document.createElement('tr');
                 
                if(shirtNumber == 0){
                    elementNew.innerHTML = `
                    <td width="20%">${name}</td>
                    <td width="15%">-</td>
                    ` ;
                } else{
                    elementNew.innerHTML = `
                    <td width="20%">${name}</td>
                    <td width="15%">${shirtNumber}</td>
                    ` ;
                }
                
                element.append(elementNew);
            });
            
            
            
        }
    }
    //Tour
    class Tournament {
        constructor( matchId, tournamentId, date, stageName, stageNumber, stadium, homeTeam, homeTeamGoals, awayTeam, awayTeamGoals, winConditions, referee, parentSelector) {
            this.matchId = matchId;
            this.tournamentId = tournamentId;
            this.date = date;
            this.stageName = stageName;
            this.stageNumber = stageNumber;
            this.stadium = stadium;
            this.homeTeam = homeTeam;
            this.homeTeamGoals = homeTeamGoals;
            this.awayTeam = awayTeam;
            this.awayTeamGoals = awayTeamGoals;
            this.winConditions = winConditions;
            this.referee = referee;
            this.parentSelector = parentSelector;
            this.parent = document.querySelector(parentSelector);
        }
        render() {
           
            const element = document.createElement('tr');
            selectMain.classList.add("hide");
            tour.forEach(item => {
                item.classList.add('hide');
            });
            element.innerHTML = `
            <td width="20%">${this.date}</td>
            <td width="15%">${this.stageName}</td>
            <td class="team-name ${this.homeTeam.name} ${this.tournamentId}" width="15%">${this.homeTeam.name}</td>
            <td width="10%">${this.homeTeamGoals} - ${this.awayTeamGoals}</td>
            <td class="team-name ${this.awayTeam.name} ${this.tournamentId}">${this.awayTeam.name}</td>
            <td width="4%"></td>
            <td width="5%">
                <span class=""></span>
            </td>
            `;
            
            this.parent.append(element);
            teamName = document.querySelectorAll('.team-name');
            
            
        }
    }
  
    //TourList
    class TournamentList {
        constructor(name, tournamentId, parentSelector) {
            this.name = name;
            this.tournamentId = tournamentId;
            this.parent = document.querySelector(parentSelector);
        }
        render() {
            const element = document.createElement('li');
            element.classList.add("tour-ect");
            element.classList.add(`${this.tournamentId}`);
            //element.innerHTML = `<a href="match.html">${this.name}</a>`;
            //element.classList.add("product-wrap");
            element.textContent = `${this.name}`;
            this.parent.append(element);
            tour = document.querySelectorAll('.tour-ect');

        }
    }


    const rel = await getResource('tournament')
    .then(data => {
        data.forEach(({name, tournamentId}) => {
            new TournamentList(name, tournamentId,".first").render();
        });
    });
    //Tour
    tour.forEach(item => {
        item.addEventListener('click', async(e) => {
            document.querySelector(".tabel-match").classList.remove("hide");
            //window.location.href=`/${item.classList.item(1)}/match`;
            const relNew = await getResource(`tournament/${item.classList.item(1)}/match`)
            .then(data => {
                 data.forEach(({ matchId, tournamentId, date, stageName, stageNumber, stadium, homeTeam, homeTeamGoals, awayTeam, awayTeamGoals, winConditions, referee}) => {
                    if(item.classList.item(1) == tournamentId){
                        new Tournament( matchId, tournamentId, date, stageName, stageNumber, stadium, homeTeam, homeTeamGoals, awayTeam, awayTeamGoals, winConditions, referee, ".tabel-match").render();
                    }
                });
            });
            setTimeout(teamNameNow(), 2000);
            
        });
       
    });
    //Team
    function teamNameNow(){
        teamName.forEach(item => {
            item.addEventListener('click', async(e) => {  
                document.querySelector(".team-match").classList.remove("hide");
                //window.location.href=`/${item.classList.item(1)}/match`;
                const relNew = await getResource(`tournament/${item.classList.item(2)}/team`)
                .then(data => {
                    data.forEach(({teamId, name, initial, players}) => {
                        if(item.classList.item(1) == name){
                            new Team( teamId, name, initial, players, ".team-match").render();
                        }
                    });
                });
            
            });

        });
    }
    //Select
    selectMain.addEventListener('click', async(e) => {  
        //window.location.href=`/${item.classList.item(1)}/match`;
        if(i%2!=0){
            i++;
            return;
        }
        i++;
        const n = selectMain.options.selectedIndex;
        tour.forEach(item => {
            item.classList.remove('hide');
        });
        const relNew = await getResource(`tournament/${Number(selectMain.options[n].text)}`)
        .then(data => {
            const tourDel = document.querySelectorAll(".tour-ect");
            data.forEach(({name, tournamentId}) => {
                tour.forEach(item => {
                    if(item.classList[1] != tournamentId){
                        item.classList.add('hide');
                    }
                });
            });
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
        formCheck.addEventListener('submit', async (e) => {
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
                .then(data =>  {
                    if (data.status == 200) {
                        adminChek();
                        showThanksModal(messageEnter.success);
                        statusMessage.remove();
                        closeModalEnter();
                        modalTrigger.classList.add('hide');
                        modalTriggerEnter.classList.add('hide');
                        let perentNav = document.querySelector(".nav-check");
                        let textEnter = document.createElement('p');
                        textEnter.classList.add("temp-p");
                        textEnter.textContent = `${formData.get("Email")}`;
                        perentNav.append(textEnter);
                        modalTriggerExit.classList.remove("hide");

                    }
                    else {
                        showThanksModal(messageEnter.failure);
                        statusMessage.remove();
                        closeModalEnter();
                        throw `error with status ${data.status}`;
                        
                    }
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
                    else {
                        statusMessage.remove();
                        closeModal();
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
    //Exit
    modalTriggerExit.addEventListener('click', async(e) => {
        let res = await fetch('account/logout');
        if (!res.ok) {
            throw new Error(`Could not fetch ${'account/logout'}, status: ${res.status}`);
        }
        document.querySelector(".temp-p").textContent='';
        modalTriggerExit.classList.add("hide");
        modalTrigger.classList.remove("hide");
        modalTriggerEnter.classList.remove("hide");
        
       
    });
    //AdminChek
    async function adminChek(){
        let res = await fetch('account/users/admin');
        if (!res.ok) {
            throw new Error(`Could not fetch ${'account/users/admin'}, status: ${res.status}`);
        } else {
            modalTriggerUsers.classList.remove("hide");
        }
        
    } 

    //AdminMain
    modalTriggerUsers.addEventListener('click', async(e) => {
        selectMain.classList.add("hide");
            tour.forEach(item => {
                item.classList.add('hide');
        });
        const relNew = await getResource(`account/users/all`)
                .then(data => {
                    data.forEach(({id, email, nickname, userRole}) => {
                        new Users(id, email, nickname, userRole, ".tble-users").render();
                    });
                });
        const btnDeleteNew = document.querySelectorAll(".btn-delete");   
        btnDeleteNew.forEach(item => {
            if(item.classList[2] == 1){
                item.classList.add("hide");
            }
            if(item.classList[1] == 1){
                item.classList.add("hide");
            }
        });
        setTimeout(deleteBtn(), 2000);

    });
    //Delete
    function deleteBtn(){
        const btnDelete = document.querySelectorAll(".btn-delete");
        const btnCh = document.querySelectorAll('.btn-ch');
        const inputCh = document.querySelectorAll('.input-ch');
        btnDelete.forEach(item => {
            item.addEventListener('click', async(e) => {
                const formCheck = new FormData();
                formCheck.append('userId', item.classList[1]);
                let res = await fetch("account/users/delete", {
                    method: "POST",
                    body: formCheck
                });
            });
        });
        btnCh.forEach(item => {
            item.addEventListener('click', (e) => {
                inputCh.forEach(i => {
                    if(item.classList[1] == i.classList[1]){
                        let formCh = new FormData();
                        formCh.append('UserId', i.classList[1]);
                        if(i.value == "ADMIN" || i.value == "admin"){
                            formCh.append('RoleID', 1);
                            console.log(1);    
                        } else{
                            formCh.append('RoleID', 0);
                            console.log(0);    
                        }                
                        
                        let res = fetch("account/users/update", {
                            method: "POST",
                            body: formCh
                        });
                    }
                });
                
                // 
                // console.log(formCh);
            });
        });
      
    }
   
   
    //Header
    sportHeader.addEventListener('click', async(e) => {
        selectMain.classList.remove("hide");
        tour.forEach(item => {
            item.classList.remove('hide');
        });
        document.querySelector(".tabel-match").innerHTML = "";
        document.querySelector(".team-match").innerHTML = "";
        document.querySelector(".tble-users").innerHTML = "";
    });
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
