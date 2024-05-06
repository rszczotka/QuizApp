const answers = document.querySelectorAll('.answer');
let chosenIndex;

const nextButton = document.querySelector('#next');
let isNextButtonDisable = true;

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');

const questionNumberDiv = document.querySelector('#question-number');

let initTime = config.totalAvailableTime; // Initial time in minutes
let time = initTime; // Current time remaining (in minutes)
const howMuchCountdown = 100; // Number of times to decrement per second (100ms)

timeDiv.innerHTML = time; // Display initial time

const countdownTime = setInterval(() => {
    // Decrement time by 1 minute divided by howMuchCountdown (effectively milliseconds per minute)
    time -= 1 / (howMuchCountdown * 60);

    // Keep time non-negative
    time = Math.max(0, time);

    // Update displayed time (round down to whole minutes if time is more than 1 minute, else show in seconds)
    if (time > 1) {
        timeDiv.innerHTML = Math.floor(time);
    } else {
        timeDiv.innerHTML = Math.floor(time * 60);
    }

    if (time > 0) {
        timeCircle.style.strokeDashoffset = Math.floor(((440 - 190) * (1 - time / initTime) + 190) * 10) / 10;
    }

    if (time < initTime * .02) {
        timeCircle.style.stroke = "#fe5c5c";
        timeDiv.style.color = "#fe5c5c";
    } else if (time < initTime * .05) {
        timeCircle.style.stroke = "#ff7400";
        timeDiv.style.color = "#ff7400";
    } else if (time < initTime * .07) {
        timeCircle.style.stroke = "#ffdf00";
        timeDiv.style.color = "#ffdf00";
    }

    // Check if time has run out (less than 1 minute remaining)
    if (time <= 0) {
        clearInterval(countdownTime);

        const fetchSystemStatus = async () => {
            try {
                const response = await fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
                const systemStatusData = await response.json();

                if (systemStatusData === 0) {
                    window.location.href = 'login.html';
                } else if (systemStatusData === 1) {
                    window.location.href = 'waiting-room.html';
                } else if (systemStatusData === 2) {
                    // status dalej jest na quizie
                } else if (systemStatusData === 3) {
                    // koniec quizu, przekieruj na ekran końcowy
                    window.location.href = 'endScreen.html';
                } else {
                    console.log('Unknown status');
                }
            } catch (error) {
                clearInterval();
                console.error(error);
                alert('Server failed to respond. Please try again later.')
                window.location.href = 'login.html';
            }
        };

        // Call the function once immediately
        fetchSystemStatus();

        // Then start the interval
        setInterval(fetchSystemStatus, 5000);
    }
}, 10);

answers.forEach((e, i) => {
    e.addEventListener('click', () => {
        if (typeof chosenIndex !== 'undefined' && chosenIndex !== null) {
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
            if (response.status === 200) {
                return response.json();
            } else if (response.status === 403) {
                console.log('System status is not 2');
                return fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`)
                    .then(response => response.json())
                    .then(systemStatusData => {
                        if (systemStatusData === 0) {
                            window.location.href = 'login.html';
                        } else if (systemStatusData === 1) {
                            window.location.href = 'login.html';
                        } else if (systemStatusData === 2) {
                            console.error('System status is 2, but server thinks that it is not!')
                            window.stop();
                        } else if (systemStatusData === 3) {
                            Alert("Czas na odpowiedź minął! Przekierowywanie na ekran końcowy.", 3, 3000, "endScreen.html")
                        } else {
                            console.log('Unknown status');
                        }
                        throw new Error('Server status 0');
                    });
            } else if (response.status === 405) {
                window.location.href = 'endScreen.html';
            } else if (response.status === 400) {
                //? User with such API key either does not exist or is not logged in
                window.location.href = 'login.html';
            } else if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            } else {
                return response.json();
            }
        })
        .then(data => {
            var answersButtons = document.querySelectorAll('.answer-disable');
            answersButtons.forEach(function (button) {
                button.classList.replace('answer-disable', 'answer');
            });
            document.querySelector('#question').innerHTML = data.text;
            currentQuestionId = data.id;
            let shuffledOptions = [...data.options].sort(() => Math.random() - 0.5);

            shuffledOptions.forEach((option, i) => {
                let originalIndex = data.options.indexOf(option);
                answers[i].innerHTML = option;
                answers[i].id = originalIndex;
            });
            time = config.totalAvailableTime - (data.time_from_beginning / 60);
            if (time <= 0) {
                Alert("Czas na odpowiedź minął! Przekierowywanie na ekran końcowy.", 3, 3000, "endScreen.html")
            }
            // console.log(time);
            questionNumberDiv.innerHTML = `${data.id}/${config.totalQuestions}`;
        })
}
GetNextQuestion()

const sendAnswer = () => {
    if (typeof chosenIndex === 'undefined') {
        chosenIndex = null;
    }

    var chosen_option = document.querySelector('.answer.chosen').id;

    answers[chosenIndex].classList.remove('chosen');
    chosenIndex = null;
    isNextButtonDisable = true;
    nextButton.classList.add('next-disable');


    var answersButtons = document.querySelectorAll('.answer');
    answersButtons.forEach(function (button) {
        button.classList.replace('answer', 'answer-disable');
    });
    document.querySelector('#question').innerHTML = "Cierpliwości! Trwa wysyłanie twoich odpowiedzi...";



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
            } else if (response.status === 405) {
                window.location.href = 'endScreen.html';
            } else if (response.status === 500) {
                Alert("Błąd podczas tworzenia odpowiedzi użytkownika!", 1)
            }

            else {
                throw new Error('Network response was not ok');
            }
        })
        .catch(error => {
            console.error('There has been a problem with your send operation:', error);
        });
}