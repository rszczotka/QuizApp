namespace quiz_app_api.Data.Entities;

public class SystemStatusEntity
{
    public int Id { get; set; }
    public int Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime UpdatedAt { get; set; }
}
