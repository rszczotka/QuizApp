using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users;

public class GetUsersInQueueJson
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("surname")]
    public required string Surname { get; set; }
	[JsonPropertyName("class")]
	public required string Class { get; set; }
}
