const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');

answers.forEach((e, i) => {
    e.addEventListener('click', () => {
        if (typeof chosenIndex !== 'undefined') {
            answers[chosenIndex].classList.remove('chosen');
        }
        e.classList.add('chosen');
        chosenIndex = i;
        isNextButtonDisable = false;
        nextButton.classList.remove('next-disable')
    });
});

nextButton.addEventListener('click', () => {
    if (!isNextButtonDisable) {
        sendAnswer();
    }
});

function getCookie(name) {
    let cookieArr = document.cookie.split("; ");
    for (let i = 0; i < cookieArr.length; i++) {
        let cookiePair = cookieArr[i].split("=");
        if (name == cookiePair[0]) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}

let api_key = getCookie('api_key');
if (api_key === null) {
    window.location.href = 'login.html';
}

function GetNextQuestion() {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    myHeaders.append("Access-Control-Allow-Origin", "*");
    myHeaders.append("Origin", "*");
    myHeaders.append("credentials", "include");

    const requestOptions = {
        method: "GET",
        headers: myHeaders,
        redirect: "follow",
    };

    fetch(`${config.api_url}/api/questions/GetNextQuestion/${api_key}`, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log('Question fetched successfully');
            document.querySelector('#question').innerHTML = data.text;
            if (response.status === 200) {
                console.log('Question fetched successfully');
                document.querySelector('#question').innerHTML = data.text;
                currentQuestionId = data.id;
                data.options.forEach((e, i) => {
                    answers[i].innerHTML = e;
                });

                timeLeft = config.totalAvailableTime - (data.time_from_beginning / 60);

                const countdownTime = setInterval(() => {
                    if (timeLeft !== undefined) {
                        timeLeft -= 1 / 60;
                        timeDiv.innerHTML = Math.floor(timeLeft + 1);
                        if (timeLeft < 0) {
                            clearInterval(countdownTime);
                            sendAnswer();
                        }
                    }
                }, 1000);
                
            } else if (response.status === 400) {
                window.location.href = 'login.html';
            } else if (response.status === 403) {
                fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`)
                    .then(response => response.json())
                    .then(systemStatusData => {
                        if (systemStatusData === 0) {
                            window.location.href = 'login.html';
                        } else if (systemStatusData === 1) {
                            window.location.href = 'waiting-room.html';
                        } else if (systemStatusData === 2) {
                            window.location.href = 'question.html';
                        } else if (systemStatusData === 3) {
                            window.location.href = 'endScreen.html';
                        } else {
                            console.log('Unknown status');
                        }
                    })
                    .catch(error => {
                        console.error('There has been a problem with your fetch operation:', error);
                    });
            }
            else if (response.status === 405) {
                window.location.href = 'endScreen.html';
            }
            else {
                throw new Error('Network response was not ok');
            }

        })
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
            if (error.message.includes('400')) {
                window.location.href = 'login.html';
            } else if (error.message.includes('403')) {
                // ... handle 403 error ...
            } else if (error.message.includes('405')) {
                window.location.href = 'endScreen.html';
            } else {
                document.querySelector('#question').innerHTML = 'There was a problem fetching the question.';
                initTime = 0;
                timeLeft = initTime;
            }
        });
}
GetNextQuestion()

const sendAnswer = () => {
    if (typeof chosenIndex === 'undefined') {
        chosenIndex = null;
    }

    var chosen_option = chosenIndex;
    console.log(chosen_option);
    const data = JSON.stringify({
        "question_id": currentQuestionId,
        "chosen_option": chosen_option,
        "api_key": api_key
    });

    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: data
    };

    fetch(`${config.api_url}/api/useranswers/CreateUserAnswer`, requestOptions)
        .then(response => {
            if (response.status === 201) {
                answers[chosenIndex].classList.remove('chosen');
                chosenIndex = null;
                isNextButtonDisable = true;
                nextButton.classList.add('next-disable')
                GetNextQuestion()
            } else if (response.status === 400) {
                window.location.href = 'login.html';
            } else if (response.status === 403) {
                const response = fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
                const systemStatusData = response.json();
                if (systemStatusData === 0) {
                    window.location.href = 'login.html';
                } else if (systemStatusData === 1) {
                    window.location.href = 'waiting-room.html';
                } else if (systemStatusData === 2) {
                    window.location.href = 'question.html';
                } else if (systemStatusData === 3) {
                    window.location.href = 'endScreen.html';
                } else {
                    console.log('Unknown status');
                }
            }
            else if (response.status === 405) {
                window.location.href = 'endScreen.html';
            }
            else {
                throw new Error('Network response was not ok');
            }
        })
        .catch(error => {
            console.error('There has been a problem with your send operation:', error);
        });
}