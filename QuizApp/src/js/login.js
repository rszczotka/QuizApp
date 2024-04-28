window.onload = function () {
    document.getElementById("form").onsubmit = function () {
        return false;
    };
};
document.cookie = "api_key=" + undefined + "; path=/;";
document.cookie = "user_id=" + undefined + "; path=/;";


const agreement = document.querySelector("#agreement");
const agreementCheckbox = document.querySelector("#agreement-checkbox");
const agreementMore = document.querySelector("#agreement-more");

let isOpenAgreement = false;

agreementMore.addEventListener('click', () => {
    if (!isOpenAgreement) {
        details.style.display = 'block';
        agreementMore.innerHTML = 'MNIEJ';
    } else {
        details.style.display = 'none';
        agreementMore.innerHTML = 'WIĘCEJ';
    }
    isOpenAgreement = !isOpenAgreement;
});

const inputs = [document.getElementById("login"), document.getElementById("password"), agreementCheckbox];

const checkAllValues = () => {
    if (inputs[0].value != '' && inputs[1].value != '' && inputs[2].checked) {
        document.querySelector('#login-button').disabled = false;
    } else {
        document.querySelector('#login-button').disabled = true;
    }
};

inputs.forEach(e => {
    if (e.type != 'checkbox') {
        e.addEventListener('input', () => {
            checkAllValues();
        });
    } else {
        e.addEventListener('click', () => {
            checkAllValues();
        });
    }
});

let api_key = getCookie('api_key');
// if (api_key !== null) {
//     window.location.href = 'info.html';
// }

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
                    Alert("Serwer ma status 0", 2)
                    throw new Error('Server status 0');
                } else if (response.status === 400) {
                    Alert("Nie znaleziono użytkownika o takim loginie i haśle, spróbuj ponownie", 1)
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
                    Alert("Serwer nie zwrócił klucza API", 2)
                }
            })
            .catch((error) => console.error(error));
    }
    else if (!agreementCheckbox.checked) {
        Alert("Zaakceptuj regulamin konkursu", 1)
    }
    else {
        Alert("Uzupełnij wszystkie pola", 3)
    }
}