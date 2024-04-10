function getCookie(name) {
    let cookieArr = document.cookie.split("; ");
    for (let i = 0; i < cookieArr.length; i++) {
        let cookiePair = cookieArr[i].split("=");
        if (name == cookiePair[0]) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}
let api_key = getCookie('api_key');
if (api_key === null) {
    window.location.href = 'login.html';
}
setInterval(async () => {
    try {
        const response = await fetch('http://localhost:5000/api/systemstatus/GetSystemStatus');
        const systemStatusData = await response.json();

        if (systemStatusData === 0) {
            window.location.href = 'login.html';
        } else if (systemStatusData === 1) {
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

            const userResponse = await fetch(`http://localhost:5000/api/users/GetUsersInQueue/${api_key}`, requestOptions);
            const userData = await userResponse.json();
            const container = document.querySelector('.kafelki-container');
            container.innerHTML = '';
            userData.forEach(user => {
                const div = document.createElement('div');
                div.className = 'kafelek';
                div.textContent = `${user.name} ${user.surname}`;
                container.appendChild(div);
            });
            const userCounter = document.getElementById('user-counter');
            const kafelekCount = document.getElementsByClassName('kafelek').length;
            userCounter.textContent = kafelekCount + " osób";
        } else if (systemStatusData === 2) {
            window.location.href = 'question.html';
        } else if (systemStatusData === 3) {
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
}, 5000);