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
	/*public List<User> Users { get; set; }

    public class User
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
    }*/
}
