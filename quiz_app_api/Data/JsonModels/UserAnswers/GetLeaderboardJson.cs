using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.UserAnswers;

public class GetLeaderboardJson
{
	[JsonPropertyName("user")]
	public required UserJson User { get; set; }
	[JsonPropertyName("correct_answers")]
	public required int CorrectAnswers { get; set; }
	[JsonPropertyName("wrong_answers")]
	public required int WrongAnswers { get; set; }

	public class UserJson
	{
		[JsonPropertyName("id")]
		public required int Id { get; set; }
		[JsonPropertyName("name")]
		public required string Name { get; set; }
		[JsonPropertyName("surname")]
		public required string Surname { get; set; }
		[JsonPropertyName("start_time")]
		public DateTime StartTime { get; set; }
		[JsonPropertyName("end_time")]
		public DateTime EndTime { get; set; }
	}
}
