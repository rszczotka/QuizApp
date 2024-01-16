const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');
const initTime = 5;
let time = initTime;
const howMuchCountdown = 100;


timeDiv.innerHTML = time;
const countdownTime = setInterval(() => {
    time-=1/howMuchCountdown
    time=Math.floor(time*howMuchCountdown)/howMuchCountdown
    timeDiv.innerHTML = Math.floor(time+1);
    timeCircle.style.strokeDashoffset = 440*(initTime-time)/initTime;
    if(time < 0){
        clearInterval(countdownTime);
        goToQuestions();
    }
}, 10);

const goToQuestions = () => {
    //go to questions
}