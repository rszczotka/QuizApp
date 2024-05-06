const http = require('http');
const url = require('url');
const port = 3000;

const server = http.createServer((req, res) => {
    const reqUrl = url.parse(req.url, true);

    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Request-Method', '*');
    res.setHeader('Access-Control-Allow-Methods', 'OPTIONS, GET');
    res.setHeader('Access-Control-Allow-Headers', '*');

    if (reqUrl.pathname.startsWith('/api/questions/GetNextQuestion/') && req.method === 'GET') {
        const id = reqUrl.pathname.split('/').pop();
        const question = {
            "text": "Pytanie",
            "options": [
                "Opcja 1",
                "Opcja 2",
                "Opcja 3",
                "Opcja 4"
            ],
            "available_time": 35,
            "id": 0
        };
        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify(question));
    } else if (reqUrl.pathname.startsWith('/api/questions/CreateAnswer/') && req.method === 'GET') {
        const parts = reqUrl.pathname.split('/');
        const api_key = parts[4];
        const chosen_option = parts[5];

        const response = {
            success: !(api_key === 'undefined')
        };

        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify(response));
    } else {
        res.writeHead(404);
        res.end(JSON.stringify({ error: "Resource not found" }));
    }
});

server.listen(port, () => {
    console.log(`Server listening at http://localhost:${port}`);
});