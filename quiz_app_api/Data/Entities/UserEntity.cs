using Newtonsoft.Json;

namespace quiz_app_api.Data.Entities;

public class UserEntity
{
    [JsonProperty("user_id")]
    public int Id { get; set; }
    [JsonProperty("account_type")]
    public int AccountType { get; set; }
    [JsonProperty("name")]
    public required string Name{ get; set; }
    [JsonProperty("surname")]
    public required string Surname { get; set; }
    [JsonProperty("login")]
    public required string Login { get; set; }
    [JsonProperty("password")]
    public required string Password { get; set; }
    [JsonProperty("api_key")]
    public required string ApiKey { get; set; }
    [JsonProperty("status")]
    public int Status { get; set; }
}
