using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users.Output;

public class GetAllUsersReturnJson
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("surname")]
	public string Surname { get; set; }
	[JsonPropertyName("login")]
	public string Login { get; set; }
	[JsonPropertyName("status")]
	public int Status { get; set; }
	[JsonPropertyName("start_time")]
	public DateTime? StartTime { get; set; }
	[JsonPropertyName("end_time")]
	public DateTime? EndTime { get; set;}
}
