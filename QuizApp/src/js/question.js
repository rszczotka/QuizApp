const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');

let initTime = 45;

const howMuchCountdown = 100;

let timeLeft;

const countdownTime = setInterval(() => {
    if (timeLeft !== undefined) {
        timeLeft -= (1 / howMuchCountdown) / 60;
        timeLeft = Math.floor(timeLeft * howMuchCountdown * 60) / (howMuchCountdown * 60);
        timeDiv.innerHTML = Math.floor(timeLeft + 1);
        timeCircle.style.strokeDashoffset = 440 - 250 * timeLeft / initTime;
        if (timeLeft < 0) {
            clearInterval(countdownTime);
            sendAnswer();
        }
    }
}, 10);

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

    fetch(`http://localhost:5000/api/questions/GetNextQuestion/${api_key}`, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            document.querySelector('#question').innerHTML = data.text;
            currentQuestionId = data.id;
            data.options.forEach((e, i) => {
                answers[i].innerHTML = e;
            });

            timeLeft = 45 - (data.time_from_beginning / 60);
        })
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
            document.querySelector('#question').innerHTML = 'There was a problem fetching the question.';
            initTime = 0;
            timeLeft = initTime;
        });
}
GetNextQuestion()

const sendAnswer = () => {
    //send answer (answer index in chosenIndex [or undefined if time is up, unless anything was selected])
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

    fetch(`http://localhost:5000/api/useranswers/CreateUserAnswer`, requestOptions)
        .then(response => {
            if (response.status === 201) {
                //* if ok, go to next question ;)
                answers[chosenIndex].classList.remove('chosen');
                GetNextQuestion()
            } else {
                throw new Error('Network response was not ok');
            }
            //TODO handle other status codes
        })
        .catch(error => {
            console.error('There has been a problem with your send operation:', error);
        });
}