namespace quiz_app_api.Data.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public int AccountType { get; set; }
    public required string Name{ get; set; }
    public required string Surname { get; set; }
    public required string Login { get; set; }
    public int Password { get; set; }
    public required string ApiKey { get; set; }
    public int Status { get; set; }
}
