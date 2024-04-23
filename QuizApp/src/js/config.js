var config = {
    api_url: 'http://192.168.55.117:5000',
    totalAvailableTime: 10,
    totalQuestions: 20,
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