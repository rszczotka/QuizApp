# QuizApp - API documentation
Wykonanie: @FilipMadzia @qWojtpl

### [Diagram bazy danych](https://dbdiagram.io/d/Festiwal_nauki_quiz_elim-65943ee2ac844320ae1cfdb2)

## CRUD
1. [Users](#users)
2. [UserAnswers](#useranswers)
3. [Questions](#questions)
4. [SystemStatus](#systemstatus)

## Url

> **Url jeszcze może się drastycznie zmienić!**

`localhost/api/{kontroler}/{metoda}`

## Users
> Create

<details><summary>CreateUser</summary>
    
### Url:

`POST: localhost/api/users/CreateUser/`

### Co przyjmuje:

W body

```json
{
    "api_key": "administrator-api-key",
    "user": {
        "name": "John",
        "surname": "Smith",
        "password": 123,
        "class": "3TP"
    }
}
```

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
500 - błąd podczas tworzenia nowego użytkownika
201 - sukces
```

</details>

> Read

<details><summary>GetAllUsers</summary>
    
### Url:

`GET: localhost/api/users/GetAllUsers/{adminApiKey}`

### Co przyjmuje:

`localhost/api/users/GetAllUsers/admin-api-key`

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
200 - sukces i tablicę użytkowników
```

```json
[
    {
        "user_id": 0,
        "name": "John",
        "surname": "Smith",
        "class": "3TP",
        "login": "john.smith",
        "password": "123",
        "status": 0,
        "end_time": "2024-04-03T21:58:16.7271061"
    },
    {
        "user_id": 1,
        "name": "William",
        "surname": "Afternoon",
        "class": "3TP",
        "login": "william.afternoon",
        "password": "123",
        "status": 0,
        "end_time": "2024-04-03T21:58:16.7271061"
    },
    {
        "user_id": 2,
        "name": "Kamil",
        "surname": "Zdun",
        "class": "3TP",
        "login": "kamil.zdun",
        "password": "123",
        "status": 0,
        "end_time": "2024-04-03T21:58:16.7271061"
    }
]
```

</details>

<details><summary>GetUsersInQueue</summary>
    
### Url:

`GET: localhost/api/users/GetUsersInQueue/{apiKey}`

### Co przyjmuje:

`localhost/api/users/GetUsersInQueue/api-key`

### Co zwraca:

```txt
400 - użytkownik o podanym API key nie istnieje lub nie jest zalogowany
200 - sukces i listę użytkowników w kolejce
```

```json
[
    {
        "user_id": 0,
        "name": "John",
        "surname": "Smith",
        "class": "3TP
    },
    {
        "user_id": 1,
        "name": "Will",
        "surname": "Hutcherson",
        "class": "3TP"
    },
    {
        "user_id": 2,
        "name": "Kamil",
        "surname": "Zdun",
        "class": "3TP"
    }
]
```

</details>

<details><summary>Login</summary>

### Url:

`POST: localhost/api/users/Login/`

### Co przyjmuje:

W body

```js
{
    "login": "login",
    "password": "password"
}
```

### Co zwraca:

```txt
400 - nie znaleziono użytkownika o takim loginie i haśle
403 - status systemu jest inny niż 1 (nie dotyczy adminów)
200 - sukces i dane użytkownika

```

```json
{
    "user_id": 0,
    "account_type": 0,
    "name": "John",
    "surname": "Smith",
    "class": "3TP",
    "login": "john.smith",
    "api_key": "some-api-key",
    "status": 0
}
```

</details>

> Update

<details><summary>UpdateUser</summary>
    
### Url:

`PUT: localhost/api/users/UpdateUser/`

### Co przyjmuje:

W body

```json
{
    "user_id": 0,
    "api_key": "admin-api-key",
    "user": {
        "name": "new-name",
        "surname": "new-surname",
        "password": "new-password",
        "class": "new-class",
        "status": 0
    }
} 
```

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
400 - nie ma użytkownika o podanym id
500 - błąd podczas aktualizowania użytkownika
204 - sukces
```

</details>

> Remove

<details><summary>RemoveUser</summary>
    
### Url:

`DELETE: localhost/api/users/RemoveUser/{adminApiKey}/{userId}`

### Co przyjmuje:

`localhost/api/users/RemoveUser/admin-api-key/user-id`

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
204 - sukces
```

</details>

## UserAnswers

> Create

<details><summary>CreateUserAnswer</summary>
    
### Url:

`POST: localhost/api/useranswers/CreateUserAnswer/`

### Co przyjmuje:

W body

```json
{
    "question_id": 1,
    "chosen_option": 1,
    "api_key": "api-key"
} 
```

### Co zwraca:

```txt
400 - użytkownik o podanym API key nie istnieje lub nie jest zalogowany
403 - status systemu jest inny niż 2
500 - błąd podczas tworzenia odpowiedzi użytkownika
201 - sukces
```

</details>

> Read

<details><summary>GetUserAnswers</summary>
    
### Url:

`GET: localhost/api/useranswers/GetUserAnswers/{api-key}/{id}`

### Co przyjmuje:

`localhost/api/useranswers/GetUserAnswers/administrator-api-key/1`

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
200 - sukces
```

```json
[
    {
        "question": {
            "text": "Pytanie",
            "options": [
                "Opcja 1",
                "Opcja 2",
                "Opcja 3",
                "Opcja 4"
            ],
            "correct_answer": 0,
            "available_time": 0
        },
        "chosen_option": 2
    },
    {
        "question": {
            "text": "Pytanie",
            "options": [
                "Opcja 1",
                "Opcja 2",
                "Opcja 3",
                "Opcja 4"
            ],
            "correct_answer": 0,
            "available_time": 0
        },
        "chosen_option": 1
    },
    {
        "question": {
            "text": "Pytanie",
            "options": [
                "Opcja 1",
                "Opcja 2",
                "Opcja 3",
                "Opcja 4"
            ],
            "correct_answer": 0,
            "available_time": 0
        },
        "chosen_option": 1
    }
]
```

</details>

<details><summary>GetLeaderboard</summary>
    
### Url:

`GET: localhost/api/useranswers/GetLeaderboard/{apiKey}`

### Co przyjmuje:

`localhost/api/useranswers/GetLeaderboard/user-api-key`

### Co zwraca:

```txt
400 - użytkownik o podanym API key nie istnieje lub nie jest zalogowany
403 - status systemu jest inny niż 3 (wyniki)
200 - sukces i listę użytkowników posortowaną malejąco według ilości poprawnych odpowiedzi oraz rosnąco według czasu zakończenia
```

```json
[
    {
        "user": {
            "id": 2,
            "name": "Kamil",
            "surname": "Zdun",
            "class": "3TP",
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
            "class": "3TP",
            "start_time": "2024-04-03T21:58:16.7271061",
            "end_time": "2024-04-10T21:19:12.9540494"
        },
        "correct_answers": 3,
        "wrong_answers": 2
    }
]
```

</details>

> Update

Nie można zmienić odpowiedzi użytkownika

> Remove

Nie można usunąć odpowiedzi użytkownika

## Questions

> Create

<details><summary>CreateQuestion</summary>
    
### Url:

`POST: localhost/api/questions/CreateQuestion/`

### Co przyjmuje:

W body

```json
{
    "api_key": "administrator-api-key",
    "question": {
        "text": "Pytanie",
        "options": [
            "Opcja 1",
            "Opcja 2",
            "Opcja 3",
            "Opcja 4"
        ],
        "correct_answer": 0,
        "available_time": 0
    }
} 
```

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
500 - coś poszło nie tak podczas dodawanie pytania do bazy danych
201 - sukces
```

</details>

> Read

<details><summary>GetAllQuestions</summary>
    
### Url:

`GET: localhost/api/questions/GetAllQuestions/{apiKey}`

### Co przyjmuje:

`localhost/api/questions/GetAllQuestions/administrator-api-key`

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
200 - tablica wszystkich pytań
```

```json
[
    {
        "question_id": 1,
        "text": "Pytanie",
        "options": [
            "Opcja 1",
            "Opcja 2",
            "Opcja 3",
            "Opcja 4"
        ],
        "correct_answer": 2,
        "available_time": 0
    },
    {
        "question_id": 2,
        "text": "Pytanie2",
        "options": [
            "Opcja 1",
            "Opcja 2",
            "Opcja 3",
            "Opcja 4"
        ],
        "correct_answer": 0,
        "available_time": 0
    }
]
```

</details>

<details><summary>GetNextQuestion</summary>
    
### Url:

`GET: localhost/api/questions/GetNextQuestion/{apiKey}`

### Co przyjmuje:

`localhost/api/questions/GetNextQuestion/api-key`

### Co zwraca:

```txt
400 - użytkownik o podanym API key nie istnieje lub nie jest zalogowany
403 - status systemu jest inny niż 2 (quiz)
405 - użytkownik odpowiedział już na wszystkie pytania
200 - następne pytanie
```

```json
{
    "id": 1,
    "text": "Pytanie",
    "options": [
        "Opcja 1",
        "Opcja 2",
        "Opcja 3",
        "Opcja 4"
    ],
    "available_time": 0,
    "time_from_beginning": 60
}
```

</details>

<details><summary>GetQuestionCount</summary>
    
### Url:

`GET: localhost/api/questions/GetQuestionCount`

### Co zwraca:

```txt
200 - ilośc pytań
```

```json
50
```

</details>

> Update

<details><summary>UpdateQuestion</summary>
    
### Url:

`PUT: localhost/api/questions/UpdateQuestion/`

### Co przyjmuje:

W body

```json
{
    "question_id": 0,
    "api_key": "administrator-api-key",
    "question": {
        "text": "Pytanie",
        "options": [
            "Opcja 1",
            "Opcja 2",
            "Opcja 3",
            "Opcja 4"
        ],
        "correct_answer": 0,
        "available_time": 0
    }
} 
```

### Co zwraca:

```txt
501 - nie zaimplementowano
```

</details>

> Remove

<details><summary>RemoveQuestion</summary>
    
### Url:

`DELETE: localhost/api/questions/RemoveQuestion/{adminApiKey}/{id}`

### Co przyjmuje:

`localhost/api/questions/RemoveQuestion/admin-api-key/question-id`

### Co zwraca:

```txt
401 - API key nie należy do admina lub admin nie zalogowany
404 - brak pytania o podanych id
204 - usunięto pomyślnie
```

</details>

## SystemStatus

> Create

Nie można stworzyć statusu systemu

> Read

<details><summary>GetSystemStatus</summary>
    
### Url:

`GET: localhost/api/systemstatus/GetSystemStatus/`

### Co zwraca:

200 - status systemu

```txt
0 - wyłączony
1 - kolejka
2 - quiz
3 - wyniki
```

</details>

> Update

<details><summary>UpdateSystemStatus</summary>
    
### Url:

`PUT: localhost/api/systemstatus/UpdateSystemStatus/`

### Co przyjmuje:

W body

```json
{
    "api_key": "administrator-api-key",
    "status": 1
}
```

Cyfrę reprezentującą status systemu:

```txt
0 - wyłączony
1 - kolejka
2 - quiz
3 - wyniki
```

### Co zwraca:

```txt
400 - złe dane wejściowe
401 - API key nie należy do admina lub admin nie zalogowany
500 - błąd podczas zmiany statusu systemu
204 - sukces
```

</details>

> Remove

Nie można usunąć statusu systemu
