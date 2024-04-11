const http = require('http');
const url = require('url');
const port = 3000;

const server = http.createServer((req, res) => {
    const reqUrl = url.parse(req.url, true);

    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Request-Method', '*');
    res.setHeader('Access-Control-Allow-Methods', 'OPTIONS, GET');
    res.setHeader('Access-Control-Allow-Headers', '*');

    if (reqUrl.pathname.startsWith('/api/users/GetUsersInQueue') && req.method === 'GET') {
        const id = reqUrl.pathname.split('/').pop();
        const question = [
            {
                "user_id": 0,
                "name": "John",
                "surname": "Smith"
            },
            {
                "user_id": 1,
                "name": "Will",
                "surname": "Hutcherson"
            },
            {
                "user_id": 2,
                "name": "Kamil",
                "surname": "Zdun"
            },
        ];
        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify(question));
    }
    else if (reqUrl.pathname.startsWith('/api/systemstatus/GetSystemStatus') && req.method === 'GET') {
        const response = {
            "status": 1
        };

        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify(response));
    } else {
        res.writeHead(404);
        res.end(JSON.stringify({ error: "server method not found" }));
    }
});

server.listen(port, () => {
    console.log(`Server listening at http://localhost:${port}`);
});