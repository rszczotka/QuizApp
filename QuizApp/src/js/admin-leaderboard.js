// setInterval(async () => {
//     try {
//         const response = await fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
//         const systemStatusData = await response.json();

//         if (systemStatusData === 3) {
//             window.location.href = 'endScreenAdmin.html';
//         } else {
//             console.log('Unknown status');
//         }
//     } catch (error) {
//         clearInterval();
//         console.error(error);
//         window.location.href = 'login.html';
//     }
// }, 5000);

const timeDiv = document.querySelector('#time');
const timeCircle = document.querySelector('#time-circle');

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
    // Check if time has run out (less than 1 minute remaining)
    if (time <= 0) {
        clearInterval(countdownTime);
        
        const fetchSystemStatus = async () => {
            try {
                const response = await fetch(`${config.api_url}/api/systemstatus/GetSystemStatus`);
                const systemStatusData = await response.json();
                if (systemStatusData === 0) {
                    window.location.href = 'login.html';
                } else if (systemStatusData === 3) {
                    window.location.href = 'endScreenAdmin.html';
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
        fetchSystemStatus();
        setInterval(fetchSystemStatus, 1000);
    }
}, 10);