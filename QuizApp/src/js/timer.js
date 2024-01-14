const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');
const initTime = 5;
let time = initTime;


timeDiv.innerHTML = time;
const countdownTime = setInterval(() => {
    timeDiv.innerHTML = --time;
    timeCircle.style.strokeDashoffset = 440*(initTime-time)/initTime;
    if(time == 0){
        clearInterval(countdownTime);
        goToQuestions();
    }
}, 1000);

const goToQuestions = () => {
    //go to questions
}