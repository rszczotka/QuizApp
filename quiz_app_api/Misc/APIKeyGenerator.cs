using System.Security.Cryptography;
using System.Text;

namespace quiz_app_api.Misc;

public class APIKeyGenerator
{

    private static readonly Dictionary<string, string> apiKeys = new Dictionary<string, string>();

    public static string GetOrGenerateAPIKey(int accountType, string login, string password)
    {
        if(apiKeys.Keys.Contains(login))
        {
            return apiKeys[login];
        }
        return GenerateAPIKey(accountType, login, password);
    }

    public static bool ContainsAPIKey(string apiKey)
    {
        return apiKeys.Values.Contains(apiKey);
    }

    public static string GenerateAPIKey(int accountType, string login, string password)
    {
        string apiKey = GetHash(login + password);
        apiKey += (char) (accountType + 97);
        apiKeys.Add(login, apiKey);
        return apiKey;
    }

    private static string GetHash(string str)
    {
        using(SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static void FlushApiKeys()
    {
        apiKeys.Clear();
    }
}
