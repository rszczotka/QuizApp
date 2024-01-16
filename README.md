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
    
### Url: `localhost/api/users/GetAllUsers/`

### Co przyjmuje:

API key użytkownika

        {
            "api_key": "some-api-key"
        }

### Co zwraca:

Wszystkich użytkowników z account_type = 0

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

</details>

<details><summary>GetUser</summary>
    
### Url: `localhost/api/users/GetUser/`

### Co przyjmuje:

Login i hasło użytkownika

        {
            "login": "john.smith",
            "password": "123"
        }

### Co zwraca:

Pojedynczego użytkownika

        {
            "user_id": 0,
            "name": "John",
            "surname": "Smith",
            "login": "john.smith",
            "password": 123,
            "api_key": "some-api-key",
            "status": 0
        }

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

> Read

> Update

> Remove