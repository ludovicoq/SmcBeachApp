namespace SmBeachApp.Entities.Models;

public class UserGroup
{
    public int UserId { get; set; }
    public int GroupId { get; set; }

    public UserDto User { get; set; }
    public GroupDto Group { get; set; }
}