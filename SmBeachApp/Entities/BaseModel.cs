namespace SmBeachApp.Entities;

public class BaseModel
{
    public DateTime DateChange { get; set; } = DateTime.Now;
    public DateTime DateCreate { get; set; } = DateTime.Now;
    public string UserRef { get; set; } = Environment.UserName;
    public string Reference { get; set; } = Environment.MachineName;

    public void UpdateBase(string? userRef = null,  string? reference = null)
    {
        DateChange = DateTime.Now;
        UserRef = userRef ?? Environment.UserName;
        Reference = reference ?? Environment.MachineName;
    }
    
}