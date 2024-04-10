window.onload = function () {
  document.getElementById("form").onsubmit = function () {
    return false;
  };
};

function getCookie(name) {
  let cookieArr = document.cookie.split("; ");
  for(let i = 0; i < cookieArr.length; i++) {
      let cookiePair = cookieArr[i].split("=");
      if(name == cookiePair[0]) {
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
  fetch('http://localhost:5000/api/systemstatus/GetSystemStatus')
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
      else {
        var login = document.getElementById("login").value;
        var password = document.getElementById("password").value;
        if (login != undefined && login != null && login != "" && password != undefined && password != null && password) {
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
          //api url
          fetch("http://localhost:5000/api/users/Login/", requestOptions)
            .then((response) => response.text())
            .then((result) => {
              var api_key = JSON.parse(result).api_key;
              if (api_key != undefined) {
                document.cookie = "api_key=" + api_key + "; path=/;";
                document.getElementById("messageBox").innerText = "udało się zalogować";
                window.location.href = "info.html";
              } else {
                document.getElementById("messageBox").innerText = "logowanie nieudane";
              }
            })
            .catch((error) => console.error(error));
        }
        else {
          document.getElementById("messageBox").innerText = "uzupełnij wszystkie pola";
        }
      }
    })
    .catch(error => {
      console.error('There has been a problem with your fetch operation:', error);
    });
}