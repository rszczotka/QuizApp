const link = document.createElement("link");
link.href = "src/css/alert.css";
link.rel = "stylesheet";
document.head.appendChild(link);


let alertContainer = document.createElement("div");
alertContainer.id = "alert-container";
document.body.appendChild(alertContainer);

const Alert = (msg, type = 0, removeTime = 0, redirect = "") => {
    let alertWindow = document.createElement("div");
    alertWindow.classList.add("alert-window");
    alertWindow.textContent = msg;
    alertContainer.appendChild(alertWindow);
    let alertClose = document.createElement("div");
    alertClose.id = "alert-close";
    let closeIcon = document.createElement("div");
    closeIcon.id = "close-icon";
    alertClose.appendChild(closeIcon);
    alertWindow.appendChild(alertClose);

    alertClose.addEventListener("click", () => {
        alertWindow.remove();
        if (!redirect == "") {
            window.location.href = redirect;
        }
    });

    if (!removeTime == 0) {
        setTimeout(() => {
            alertWindow.classList.add('fade-out');
            setTimeout(() => {
                window.location.href = redirect;
                alertWindow.remove();
            }, 5000);
        }, removeTime);
    }

    switch (type) {
        case 0:
            alertWindow.classList.add("alert-success");
            break;
        case 1:
            alertWindow.classList.add("alert-error");
            break;
        case 2:
            alertWindow.classList.add("alert-warning");
            break;
        case 3:
            alertWindow.classList.add("alert-info");
            break;
    }
}