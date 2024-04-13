window.onload = function () {
    document.getElementById("form").onsubmit = function () {
        return false;
    };
};

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
if (api_key !== null) {
    window.location.href = 'info.html';
}

function sendApiRequest() {
    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;
    if (login && password) {
        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Access-Control-Allow-Origin", "*");
        myHeaders.append("Origin", "*");
        myHeaders.append("credentials", "include");

        const data = JSON.stringify({
            login: login,
            password: password,
        });

        const requestOptions = {
            method: "POST",
            headers: myHeaders,
            body: data,
            redirect: "follow",
        };

        fetch(`${config.api_url}/api/users/Login/`, requestOptions)
            .then((response) => {
                if (response.status === 200) {
                    return response.text();
                } else if (response.status === 403) {
                    // document.querySelector('.messageBox').innerHTML = `Serwer ma status 0`
                    // document.querySelector('.messageBox').classList.add('show');
                    throw new Error('Server status 0');
                } else if (response.status === 400) {
                    // document.querySelector('.messageBox').innerHTML = `Nie znaleziono użytkownika o takim loginie i haśle`
                    // document.querySelector('.messageBox').classList.add('show');
                    throw new Error('User not found');
                } else {
                    throw new Error('Network response was not ok');
                }
            })
            .then((result) => {
                var api_key = JSON.parse(result).api_key;
                var user_id = JSON.parse(result).user_id;
                if (api_key != undefined) {
                    document.cookie = "api_key=" + api_key + "; path=/;";
                    document.cookie = "user_id=" + user_id + "; path=/;";
                    
                    window.location.href = "info.html";
                } else {
                    // document.querySelector('.messageBox').innerHTML = `logowanie nieudane`
                    // document.querySelector('.messageBox').classList.add('show');
                }
            })
            .catch((error) => console.error(error));
    }
    else {
        // document.getElementById("messageBox").innerText = "uzupełnij wszystkie pola";
    }
}