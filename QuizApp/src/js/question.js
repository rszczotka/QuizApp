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

const getCookie = (name) => {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

let api_key = getCookie('api_key');

//! REMOVE THIS IF STATEMENT ONCE WE HAVE LOGIN WORKING
if (typeof api_key === 'undefined') {
    console.log('api_key is undefined');
    api_key = 'test-api-key';
}
var questionId = 1;
function GetQuestionById() {
    const questionJSON = fetch(`http://localhost:3000/api/questions/GetNextQuestion/${api_key}`)
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
        })
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
            document.querySelector('#question').innerHTML = 'There was a problem fetching the question.';
            initTime = 0;
            time = initTime;
        });
}
GetQuestionById()


const sendAnswer = () => {
    //send answer (answer index in chosenIndex [or undefined if time is up, unless anything was selected])
    var chosen_option = chosenIndex;
    const CreateAnswer = fetch(`http://localhost:3000/api/questions/CreateAnswer/${api_key}/${chosen_option}`)
        .then(response => {
            if (response.ok) {
                //* if ok, go to next question ;)
                GetQuestionById()
            } else {
                throw new Error('Network response was not ok');
            }

            return response.json();
        })
        .catch(error => {
            console.error('There has been a problem with your send operation:', error);
        });
}