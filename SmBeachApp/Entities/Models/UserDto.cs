namespace SmBeachApp.Entities.Models;

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ActiveType ActiveType { get; set; }
    public string Email { get; set; }

    public List<RoleDto> Roles { get; set; }

    public List<GroupDto> Groups { get; set; }
}