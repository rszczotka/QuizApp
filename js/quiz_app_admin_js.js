var admin_key = getCookie("api_key");
var xhr = new XMLHttpRequest();
      xhr.withCredentials = true;

      xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
          console.log(this.responseText);
          var x = this.responseText;
          document.getElementById("system status").innerText =
            this.responseText;
        }
      });

      xhr.open(
        "GET",
        "http://localhost:5000/api/systemstatus/GetSystemStatus/"
      );
      xhr.setRequestHeader("Content-Type", "application/json");
      xhr.send();
      function send_update(value) {
        var data = JSON.stringify({
          api_key:
            admin_key,
          status: 1,
        });
        console.log(data);
        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener("readystatechange", function () {
          if (this.readyState === value) {
            console.log(this.responseText);
          }
        });

        xhr.open(
          "PUT",
          "http://localhost:5000/api/systemstatus/UpdateSystemStatus/"
        );
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);
      }

      function getCookie(cname) {
        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for(let i = 0; i <ca.length; i++) {
          let c = ca[i];
          while (c.charAt(0) == ' ') {
            c = c.substring(1);
          }
          if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
          }
        }
        return "";
      }
