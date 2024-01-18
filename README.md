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

`localhost/api/users/{metoda}`

## Users
> Create

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

> Remove

## UserAnswers

> Create

> Read

> Update

> Remove

## Questions

> Create

> Read

> Update

> Remove

## SystemStatus

> Create

Nie ma możliwości tworzenia nowego statusu

> Read

<details><summary>GetSystemStatus</summary>
    
### Url:

`localhost/api/users/GetSystemStatus/`

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

`localhost/api/users/UpdateSystemStatus/`

### Co przyjmuje:

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

Informację o sukcesie lub porażce

```json
{
    "succeeded": true
}
```

</details>

> Remove

Nie można usunąć statusu systemu