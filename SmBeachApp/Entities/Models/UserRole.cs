namespace SmBeachApp.Entities.Models;

public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public UserDto User { get; set; }
    public RoleDto Role { get; set; }
}