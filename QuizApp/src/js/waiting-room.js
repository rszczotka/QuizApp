setInterval(async () => {
    try {
        const response = await fetch('http://localhost:3000/api/systemstatus/GetSystemStatus');
        const data = await response.json();
        const status = data.status;

        switch (status) {
            case 0:
                window.location.href = 'login.html';
                break;
            case 1:
                const userResponse = await fetch('http://localhost:3000/api/users/GetUsersInQueue');
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
                break;
            case 2:
                window.location.href = 'question.html';
                break;
            case 3:
                window.location.href = 'endScreen.html';
                break;
            default:
                console.log('Unknown status');
        }
    } catch (error) {
        clearInterval();
        console.error(error);
        alert('Server failed to respond. Please try again later.')
        window.location.href = 'login.html';
    }
}, 5000);