const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');
const initTime = 30;
let time = initTime;
const howMuchCountdown = 100;

timeDiv.innerHTML = time;
const countdownTime = setInterval(() => {
    time-=1/howMuchCountdown
    time=Math.floor(time*howMuchCountdown)/howMuchCountdown
    timeDiv.innerHTML = Math.floor(time+1);
    timeCircle.style.strokeDashoffset = 440-250*time/initTime;
    if(time < 0){
        clearInterval(countdownTime);
        sendAnswer();
    }
}, 10);

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