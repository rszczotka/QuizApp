let leaderboardExample = [
    {"id": 0, "name": "A0", "time": "B", "points": "C" },
    {"id": 1, "name": "A1", "time": "B", "points": "C" },
    {"id": 2, "name": "A2", "time": "B", "points": "C" },
    {"id": 3, "name": "A3", "time": "B", "points": "C" },
    {"id": 4, "name": "A4", "time": "B", "points": "C" },
    {"id": 5, "name": "A5", "time": "B", "points": "C" },
    {"id": 6, "name": "A6", "time": "B", "points": "C" },
    {"id": 7, "name": "A7", "time": "B", "points": "C" },
    {"id": 8, "name": "A8", "time": "B", "points": "C" },
    {"id": 9, "name": "A9", "time": "B", "points": "C" },
    {"id": 10, "name": "A10", "time": "B", "points": "C" },
    {"id": 11, "name": "A11", "time": "B", "points": "C" },
    {"id": 12, "name": "A12", "time": "B", "points": "C" },
    {"id": 13, "name": "A13", "time": "B", "points": "C" },
    {"id": 14, "name": "A14", "time": "B", "points": "C" },
    {"id": 15, "name": "A15", "time": "B", "points": "C" },
    {"id": 16, "name": "A16", "time": "B", "points": "C" },
];

const leaderboard = document.querySelector("#leaderboard");

let leaderboardText = "";

const userId = 12;

leaderboardExample.forEach(e => {
    if(e.id == userId){
        leaderboardText+=`
        <div class="row your-score">
            <div class="name">${e.name}</div>
            <div class="points">${e.time}</div>
            <div class="time">${e.points}</div>
        </div>
        `
    }else{
        leaderboardText+=`
        <div class="row">
            <div class="name">${e.name}</div>
            <div class="points">${e.time}</div>
            <div class="time">${e.points}</div>
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

