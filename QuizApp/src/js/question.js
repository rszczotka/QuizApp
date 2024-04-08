const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');
let initTime = 30;
let time = initTime;
const howMuchCountdown = 100;

timeDiv.innerHTML = time;
const countdownTime = setInterval(() => {
    time -= 1 / howMuchCountdown
    time = Math.floor(time * howMuchCountdown) / howMuchCountdown
    timeDiv.innerHTML = Math.floor(time + 1);
    timeCircle.style.strokeDashoffset = 440 - 250 * time / initTime;
    if (time < 0) {
        clearInterval(countdownTime);
        sendAnswer();
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
    for(let i = 0; i < cookieArr.length; i++) {
        let cookiePair = cookieArr[i].split("=");
        if(name == cookiePair[0]) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}

let api_key = getCookie('api_key');

console.log(api_key);

function GetNextQuestion() {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    myHeaders.append("Access-Control-Allow-Origin", "*");
    myHeaders.append("Origin", "*");
    myHeaders.append("credentials", "include");

    const data = JSON.stringify({
        api_key: api_key
    });

    const requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: data,
        redirect: "follow",
    };

    fetch(`http://localhost:5000/api/questions/GetNextQuestion`, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            document.querySelector('#question').innerHTML = data.text;
            data.options.forEach((e, i) => {
                answers[i].innerHTML = e;
            });
            initTime = data.available_time;
            time = initTime;
            // ! implement new time logic
        })
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
            document.querySelector('#question').innerHTML = 'There was a problem fetching the question.';
            initTime = 0;
            time = initTime;
        });
}
GetNextQuestion()


const sendAnswer = () => {
    //send answer (answer index in chosenIndex [or undefined if time is up, unless anything was selected])
    if (typeof chosenIndex === 'undefined') {
        chosenIndex = null;
    }

    var chosen_option = chosenIndex;
    const CreateAnswer = fetch(`http://localhost:3000/api/questions/CreateAnswer/${api_key}/${chosen_option}`)
        .then(response => {
            if (response.ok) {
                //* if ok, go to next question ;)
                answers[chosenIndex].classList.remove('chosen');
                GetNextQuestion()
            } else {
                throw new Error('Network response was not ok');
            }

            return response.json();
        })
        .catch(error => {
            console.error('There has been a problem with your send operation:', error);
        });
}