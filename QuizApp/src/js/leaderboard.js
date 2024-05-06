let api_key = getCookie('api_key');
let user_id = parseInt(getCookie('user_id'));

if (api_key === null) {
    window.location.href = 'login.html';
}


function sendApiRequest() {
fetch(`${config.api_url}/api/systemstatus/GetSystemStatus/`)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
        })
        .then(systemStatusData => {
        if (systemStatusData === 0) {
            console.log('Server is down');
        }
        else if (systemStatusData === 1) {
            window.location.href = 'login.html';
        }
        else if (systemStatusData === 2) {
            window.location.href = 'question.html';
        } else if (systemStatusData === 3) {
            fetch(`${config.api_url}/api/useranswers/GetLeaderboard/${api_key}`)
                .then((response) => response.text())
                .then((result) => {
                    var result = JSON.parse(result);

                    let i = 0;
                    let leaderboardArr = [];
                    result.forEach(e => {
                        let name = `${e.user.name} ${e.user.surname}`;

                        let start_time = Date.parse(e.user.start_time);
                        let end_time = Date.parse(e.user.end_time);
                        let time = end_time - start_time;
                        let timeMin = Math.floor(time / (1000 * 60));
                        let timeSec = Math.floor(time % (1000 * 60) / 1000);
                        let timeMs = Math.floor(time % 1000);
                        let timeStr = `${timeMin}min ${timeSec}s ${timeMs}ms`;

                        let correct_answers = `${e.correct_answers}/${config.totalQuestions}`;

                        leaderboardArr.push({
                            "id": e.user.id,
                            "name": name,
                            "time": timeStr,
                            "points": correct_answers
                        });

                        i++;
                    });

                    createView(leaderboardArr, user_id);
                })
                .catch((error) => console.error(error));
        } else {
            console.log('Unknown status');
        }
    })
    .catch(error => {
        console.error('There has been a problem with your fetch operation:', error);
    });
}

sendApiRequest();

const createView = (leaderboardArr, id) => {
    
    const leaderboard = document.querySelector("#leaderboard");
    
    let leaderboardText = "";
    
    const userId = id;
    
    let i = 0;
    leaderboardArr.forEach(e => {
        if(e.id == userId){
            leaderboardText+=`
            <div class="row your-score">
                <div class="name">${e.name}</div>
                <div class="time">${e.time}</div>
                <div class="points">${e.points}</div>
                <div class="place">${++i}.</div>
            </div>
            `
        }else{
            leaderboardText+=`
            <div class="row">
                <div class="name">${e.name}</div>
                <div class="time">${e.time}</div>
                <div class="points">${e.points}</div>
                <div class="place">${++i}.</div>
            </div>
            `
        }
    });
    
    leaderboard.innerHTML = leaderboardText;
    
    const yourScore = document.querySelector(".your-score");
    
    if(yourScore){
        yourScore.addEventListener("click", () => {
            yourScore.scrollIntoView({behavior: 'smooth'});
        });
    }
    
    const goTop = document.querySelector("#go-top");
    
    goTop.addEventListener("click", () => {
        document.querySelector("h1").scrollIntoView({behavior: 'smooth'});
    });
    
    const whenShowGoTopButton = 1000;
    
    setInterval(() => {
        var scroll = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
        if(scroll < whenShowGoTopButton){
            goTop.style.opacity = scroll / whenShowGoTopButton;
        }else{
            goTop.style.opacity = "1";
        }
    }, 100);
}