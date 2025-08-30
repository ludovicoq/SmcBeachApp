namespace SmBeachApp.Entities;

public class Group : BaseModel
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ActiveType Status { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RoleGroup> RoleGroups { get; set; } = new List<RoleGroup>();
    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}