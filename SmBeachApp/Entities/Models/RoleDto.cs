namespace SmBeachApp.Entities.Models;

public class RoleDto
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ActiveType Status { get; set; }
}