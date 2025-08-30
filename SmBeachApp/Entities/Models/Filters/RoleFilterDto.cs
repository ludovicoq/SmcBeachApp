namespace SmBeachApp.Entities.Models.Filters;

public class RoleFilterDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ActiveType? Status { get; set; }
}