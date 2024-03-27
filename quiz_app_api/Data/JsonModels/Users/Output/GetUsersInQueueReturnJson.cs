using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users.Output;

public class GetUsersInQueueReturnJson
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("surname")]
	public string Surname { get; set; }
}
