namespace SmBeachApp.Entities;

public class RoleGroup
{
    public int RoleId { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public Role Role { get; set; }
}