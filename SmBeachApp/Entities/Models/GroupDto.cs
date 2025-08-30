namespace SmBeachApp.Entities.Models;

public class GroupDto
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ActiveType Status { get; set; }
}