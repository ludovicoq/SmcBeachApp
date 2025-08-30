namespace SmBeachApp.Entities;

public class User : BaseModel
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public string? Email { get; set; }

    public ActiveType ActiveType { get; set; }
    
    // public ICollection<Role> Roles { get; set; } = new List<Role>();
    //
    // public ICollection<Group> Groups { get; set; } = new List<Group>();
    
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

}