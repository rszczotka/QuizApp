const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');

const questionNumberDiv = document.querySelector('#question-number');

const countdownTime = setInterval(() => {
    if (timeLeft !== undefined) {
        timeLeft -= 1 / 60;
        timeDiv.innerHTML = Math.floor(timeLeft + 1);
        if (timeLeft < 0) {
            // clearInterval(countdownTime);
            // sendAnswer();
        }
    }
}, 100);

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
            document.querySelector('#question').innerHTML = data.text;
            currentQuestionId = data.id;
            data.options.forEach((e, i) => {
                answers[i].innerHTML = e;
            });
            timeLeft = config.totalAvailableTime - (data.time_from_beginning / 60);
            console.log(data.id, config.totalQuestions);
            questionNumberDiv.innerHTML = `${data.id}/${config.totalQuestions}`;

            // document.querySelectorAll('.question-number').innerHTML = `${data.id}/${config.totalQuestions}`
            //TODO hide loader
        })
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
            if (error.message.includes('400')) {
                window.location.href = 'login.html';
            } else if (error.message.includes('403')) {
                console.log('System status is not 2');
                try {
                    const response = fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
                    const systemStatusData = response.json();

                    if (systemStatusData === 0) {
                        window.location.href = 'login.html';
                    } else if (systemStatusData === 1) {
                        window.location.href = 'login.html';
                    } else if (systemStatusData === 2) {
                        console.error('System status is 2, but server thinks that it is not!')
                        window.stop();
                    } else if (systemStatusData === 3) {
                        //TODO show popup that time is over and redirect user to endScreen
                        window.location.href = 'endScreen.html';
                    } else {
                        console.log('Unknown status');
                    }
                } catch (error) {
                    console.error(error);
                    alert('Server failed to respond. Please try again later.')
                    window.location.href = 'login.html';
                }
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
                // answers[chosenIndex].classList.remove('chosen');
                // chosenIndex = null;
                // isNextButtonDisable = true;
                // nextButton.classList.add('next-disable')
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