using quiz_app_api.Data;

namespace quiz_app_api.Misc;

public class AdminTools
{
	public static bool IsUser(string apiKey)
	{
		return APIKeyGenerator.GetLoginByAPIKey(apiKey) != null;
	}

	public static bool IsAdmin(string apiKey)
	{
		return apiKey.EndsWith((char)98) && APIKeyGenerator.ContainsAPIKey(apiKey);
	}
}
