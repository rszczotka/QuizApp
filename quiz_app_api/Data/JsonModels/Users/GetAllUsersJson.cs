using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users;

public class GetAllUsersJson
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("surname")]
    public required string Surname { get; set; }
	[JsonPropertyName("class")]
	public required string Class { get; set; }
	[JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("password")]
    public required string Password { get; set; }
    [JsonPropertyName("status")]
    public required int Status { get; set; }
    [JsonPropertyName("end_time")]
    public required DateTime EndTime { get; set; }
}
