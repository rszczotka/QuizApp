using CsvHelper.Configuration.Attributes;

namespace quiz_app_api.Data.Seeds.Users;

public class UserCsv
{
	[Index(0)]
	public required string FirstName { get; set; }
	[Index(1)]
	public required string LastName { get; set; }
	[Index(2)]
	public required string Class { get; set; }
}
