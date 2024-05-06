const http = require('http');
const url = require('url');
const port = 3000;

const server = http.createServer((req, res) => {
    const reqUrl = url.parse(req.url, true);

    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Request-Method', '*');
    res.setHeader('Access-Control-Allow-Methods', 'OPTIONS, GET');
    res.setHeader('Access-Control-Allow-Headers', '*');

    if (reqUrl.pathname.startsWith('/api/useranswers/GetLeaderboard/4848734398e318adb7babb90de5d7828d8fcf897a823d96965935b5e246e41b4b') && req.method === 'GET') {
        const id = reqUrl.pathname.split('/').pop();
        const question = [
            {
                "user": {
                    "id": 4231,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 5,
                    "name": "Adam",
                    "surname": "Nowak",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:19:12.9540494"
                },
                "correct_answers": 3,
                "wrong_answers": 2
            },
            {
                "user": {
                    "id": 223,
                    "name": "Kamill",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 21,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 23,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 24,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 55,
                    "name": "Adam",
                    "surname": "Nowak",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:19:12.9540494"
                },
                "correct_answers": 3,
                "wrong_answers": 2
            },
            {
                "user": {
                    "id": 26,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 27,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 28,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 59,
                    "name": "Adam",
                    "surname": "Nowak",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:19:12.9540494"
                },
                "correct_answers": 3,
                "wrong_answers": 2
            },
            {
                "user": {
                    "id": 2,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            },
            {
                "user": {
                    "id": 212,
                    "name": "Kamil",
                    "surname": "Zdun",
                    "start_time": "2024-04-03T21:58:16.7271061",
                    "end_time": "2024-04-10T21:15:55.9540494"
                },
                "correct_answers": 4,
                "wrong_answers": 1
            }
        ];
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