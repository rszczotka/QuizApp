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

`localhost/api/{tabela}/{metoda}`

## Users
> Create

<details><summary>CreateUser</summary>
    
### Url:

`localhost/api/users/CreateUser/`

### Co przyjmuje:

JSON nowego użytkownika i API key (administrator)

```json
{
    "api_key": "administrator-api-key",
    "user": {
        "account_type": 0,
        "name": "John",
        "surname": "Smith",
        "login": "john.smith",
        "password": 123,
        "api_key": "some-api-key",
        "status": 0
    }
}
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

> Read

<details><summary>GetAllUsers</summary>
    
### Url:

`localhost/api/users/GetAllUsers/`

### Co przyjmuje:

API key użytkownika

```json
{
    "api_key": "some-api-key"
}
```

### Co zwraca:

Wszystkich użytkowników z account_type = 0

```json
[
    {
        "user_id": 0,
        "name": "John",
        "surname": "Smith",
        "login": "john.smith",
        "password": 123,
        "api_key": "some-api-key",
        "status": 0
    },
    {
        "user_id": 1,
        "name": "Will",
        "surname": "Hutcherson",
        "login": "will.hutcherson",
        "password": 234,
        "api_key": "some-api-key",
        "status": 0
    },
    {
        "user_id": 2,
        "name": "Kamil",
        "surname": "Zdun",
        "login": "kamil.zdun",
        "password": 345,
        "api_key": "some-api-key",
        "status": 0
    },
    ...
]
```

</details>

<details><summary>LogIn</summary>
    
### Url:

`localhost/api/users/LogIn/`

### Co przyjmuje:

Login i hasło użytkownika z użyciem POST

### Co zwraca:

Dane użytkownika

```json
{
    "logged_in": true,
    "user_id": 0,
    "name": "John",
    "surname": "Smith",
    "login": "john.smith",
    "password": 123,
    "api_key": "some-api-key",
    "status": 0
}
```

</details>

> Update

<details><summary>UpdateUser</summary>
    
### Url:

`localhost/api/users/UpdateUser/`

### Co przyjmuje:

ID użytkownika, dane użytkownika, API key (administrator)

```json
{
    "user_id": 0,
    "api_key": "administrator-api-key",
    "user": {
        "name": "new-name",
        "surname": "new-surname",
        "login": "new-login",
        "password": "new-password",
        "api_key": "some-api-key",
        "status": 0
    }
} 
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

<details><summary>UpdateUserStatus</summary>
    
### Url:

`localhost/api/users/UpdateUser/`

### Co przyjmuje:

ID użytkownika, status użytkownika, API key

```json
{
    "user_id": 0,
    "api_key": "api-key",
    "status": 0
} 
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

> Remove

<details><summary>RemoveUser</summary>
    
### Url:

`localhost/api/users/RemoveUser/`

### Co przyjmuje:

ID użytkownika, API key (administrator)

```json
{
    "user_id": 0,
    "api_key": "administrator-api-key"
} 
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

## UserAnswers

> Create

> Read

> Update

> Remove

<details><summary>RemoveUserAnswer</summary>
    
### Url:

`localhost/api/useranswers/RemoveUserAnswer/`

### Co przyjmuje:

ID odpowiedzi użytkownika, API key (administrator)

```json
{
    "result_id": 0,
    "api_key": "administrator-api-key"
} 
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

## Questions

> Create

<details><summary>CreateQuestion</summary>
    
### Url:

`localhost/api/questions/CreateQuestion/`

### Co przyjmuje:

Pytanie, API key (administrator)

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

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

> Read

<details><summary>GetQuestionById</summary>
    
### Url:

`localhost/api/questions/GetQuestionById/`

### Co przyjmuje:

ID pytania, API key

```json
{
    "question_id": 0,
    "api_key": "administrator-api-key"
} 
```

### Co zwraca:

Pytanie

```json
{
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
```

</details>

> Update

<details><summary>UpdateQuestion</summary>
    
### Url:

`localhost/api/questions/UpdateQuestion/`

### Co przyjmuje:

ID pytania, pytanie, API key (administrator)

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

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

> Remove

<details><summary>RemoveQuestion</summary>
    
### Url:

`localhost/api/questions/RemoveQuestion/`

### Co przyjmuje:

ID pytania, API key (administrator)

```json
{
    "question_id": 0,
    "api_key": "administrator-api-key"
} 
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

## SystemStatus

> Create

Nie można stworzyć statusu systemu

> Read

<details><summary>GetSystemStatus</summary>
    
### Url:

`localhost/api/systemstatus/GetSystemStatus/`

### Co przyjmuje:

Nic

### Co zwraca:

Status systemu

```json
{
    "status": 0
}
```

</details>

> Update

<details><summary>UpdateSystemStatus</summary>
    
### Url:

`localhost/api/systemstatus/UpdateSystemStatus/`

### Co przyjmuje:

API key admina

```json
{
    "api_key": "some-api-key"
}
```

Cyfrę reprezentującą status systemu:

- 0 - wyłączony
- 1 - poczekalnia
- 2 - quiz
- 3 - wyniki

```json
{
    "system_status": 1
}
```

### Co zwraca:

Informację o sukcesie

```json
{
    "succeeded": true
}
```

</details>

> Remove

Nie można usunąć statusu systemu
