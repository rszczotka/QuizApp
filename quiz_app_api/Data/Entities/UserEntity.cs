namespace quiz_app_api.Data.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public required int AccountType { get; set; }
    public required string Name{ get; set; }
    public required string Surname { get; set; }
    public required string Class { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required int Status { get; set; }
    public DateTime EndTime { get; set; }
}
