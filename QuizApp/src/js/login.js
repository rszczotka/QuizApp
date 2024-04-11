window.onload = function () {
  document.getElementById("form").onsubmit = function () {
    return false;
  };
};
function sendApiRequest() {
  var login = document.getElementById("login").value;
  var password = document.getElementById("password").value;
  if (
    login != undefined &&
    login != null &&
    login != "" &&
    password != undefined &&
    password != null &&
    password
  ) {
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
        var user_id = JSON.parse(result).user_id;
        console.log(user_id);
        if (api_key != undefined) {
          document.cookie = "api_key=" + api_key + ";";
          if (user_id != undefined) document.cookie = "id=" + user_id + ";";
          document.getElementById("messageBox").innerText =
            "udało się zalogować";
        } else {
          document.getElementById("messageBox").innerText =
            "logowanie nieudane";
        }
      })
      .catch((error) => console.error(error));
  } else {
    document.getElementById("messageBox").innerText =
      "uzupełnij wszystkie pola";
  }
}
