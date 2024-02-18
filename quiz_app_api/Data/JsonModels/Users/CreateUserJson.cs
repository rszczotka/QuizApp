using Newtonsoft.Json;

namespace quiz_app_api.Data.JsonModels.Users;

public class CreateUserJson
{
    [JsonProperty("api_key")]
    public string? ApiKey { get; set; }
    [JsonProperty("user")]
    public UserJson? User { get; set; }
}

public class UserJson
{
    [JsonProperty("name")]
    public required string Name { get; set; }
    [JsonProperty("surname")]
    public required string Surname { get; set; }
    [JsonProperty("password")]
    public required string Password { get; set; }
}

// {"api_key": "system", "user": {"name": "create", "surname": "test", "password": "123"}}