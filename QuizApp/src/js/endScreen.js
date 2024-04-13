const showLeaderboard = document.querySelector("#show-leaderboard");

setInterval(async () => {
    try {
        const response = await fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
        const systemStatusData = await response.json();

        if (systemStatusData === 0) {
            window.location.href = 'login.html';
        } else if (systemStatusData === 1) {
            window.location.href = 'login.html';
        } else if (systemStatusData === 2) {
            showLeaderboard.innerHTML = "Poczekaj na zakończenie testu";
            showLeaderboard.style.backgroundColor = "#3b3b3b";
        } else if (systemStatusData === 3) {
            showLeaderboard.innerHTML = "Zobacz tabele wyników";
            showLeaderboard.style.backgroundColor = "var(--primary)";

            showLeaderboard.addEventListener("click", () => {
                window.open("./leaderboard.html"); 
            });

        } else {
            console.log('Unknown status');
        }
    } catch (error) {
        clearInterval();
        console.error(error);
        alert('Server failed to respond. Please try again later.')
        window.location.href = 'login.html';
    }
}, 5000);