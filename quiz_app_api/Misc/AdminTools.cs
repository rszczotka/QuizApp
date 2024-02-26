namespace quiz_app_api.Misc;

public class AdminTools
{
	public static bool IsAdmin(string apiKey)
	{
		return apiKey.EndsWith((char)98) && APIKeyGenerator.ContainsAPIKey(apiKey);
	}
}
