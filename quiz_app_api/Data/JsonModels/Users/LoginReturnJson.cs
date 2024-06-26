﻿using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users;

public class LoginReturnJson
{
    [JsonPropertyName("user_id")]
    public required int Id { get; set; }
    [JsonPropertyName("account_type")]
    public required int AccountType { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("surname")]
    public required string Surname { get; set; }
	[JsonPropertyName("class")]
	public required string Class { get; set; }
	[JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }
    [JsonPropertyName("status")]
    public required int Status { get; set; }
}
