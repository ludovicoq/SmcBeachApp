using SmBeachApp.Entities;
using SmBeachApp.Entities.Models;

namespace SmBeachApp.Extensions.DbExtensions;

public static class AuthExtension
{
    public static IQueryable<RoleDto> ToRoleDto(this IQueryable<Role> roles)
    {
        return roles.Select(r => new RoleDto()
        {
            RoleId = r.RoleId,
            Name = r.Name,
            Description = r.Description,
            Status = r.Status
        });
    }
    
    public static IQueryable<GroupDto> ToGroupDto(this IQueryable<Group> roles)
    {
        return roles.Select(r => new GroupDto()
        {
            GroupId = r.GroupId,
            Name = r.Name,
            Description = r.Description,
            Status = r.Status
        });
    }
}