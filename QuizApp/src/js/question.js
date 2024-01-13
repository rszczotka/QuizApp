const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');
const initTime = 30;
let time = initTime;


timeDiv.innerHTML = time;
const countdownTime = setInterval(() => {
    timeDiv.innerHTML = --time;
    timeCircle.style.strokeDashoffset = 440-250*time/initTime;
    if(time == 0){
        clearInterval(countdownTime);
        sendAnswer();
    }
}, 1000);

answers.forEach((e, i) => {
    e.addEventListener('click', () => {
        if(typeof chosenIndex !== 'undefined'){
            answers[chosenIndex].classList.remove('chosen');
        }
        e.classList.add('chosen');
        chosenIndex = i;
        isNextButtonDisable = false;
        nextButton.classList.remove('next-disable')
    });
});

nextButton.addEventListener('click', () => {
    if(!isNextButtonDisable){
        sendAnswer();
    }
});

const sendAnswer = () => {
    //send answer (answer index in chosenIndex [or undefined if time is up, unless anything was selected])
}