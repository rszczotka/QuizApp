let api_key = getCookie('api_key');
if (api_key === null) {
    window.location.href = 'login.html';
}

async function fetchSystemStatus() {
    try {
        const response = await fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
        const systemStatusData = await response.json();

        if (systemStatusData === 0) {
            window.location.href = 'login.html';
        } else if (systemStatusData === 1) {
            window.location.href = 'login.html';
        } else if (systemStatusData === 2) {
            //endScrean.html
        } else if (systemStatusData === 3) {
            window.location.href = 'leaderboard.html';
            clearInterval(intervalId);
        } else {
            console.log('Unknown status');
        }
    } catch (error) {
        clearInterval();
        console.error(error);
        alert('Server failed to respond. Please try again later.')
        window.location.href = 'login.html';
    }
}

// Fetch system status once the site loads
fetchSystemStatus();

// Then start the interval loop
let intervalId = setInterval(fetchSystemStatus, 5000);