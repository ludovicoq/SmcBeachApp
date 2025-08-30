using System.Net;
using Microsoft.EntityFrameworkCore;
using SmBeachApp.Data;
using SmBeachApp.Entities;
using SmBeachApp.Entities.Models;
using SmBeachApp.Entities.Models.Filters;
using SmBeachApp.Extensions.DbExtensions;
using SmBeachApp.Localization.Localization;

namespace SmBeachApp.Services;

public class RoleService
{
    private readonly SmBeachDbContext _ctx;

    public RoleService(SmBeachDbContext context)
    {
        _ctx = context;
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        return await _ctx.Roles
            .AsNoTracking()
            .ToRoleDto()
            .ToListAsync();
    }

    public async Task<RoleDto> GetRoleByIdAsync(int roleId)
    {
        return await _ctx.Roles
            .AsNoTracking()
            .Where(r => roleId == r.RoleId)
            .ToRoleDto()
            .FirstOrDefaultAsync();
    }

    public async Task<PageResultDto<List<RoleDto>>> SearchRolesAsync(RoleFilterDto filter, int page, int size)
    {
        var query = _ctx.Roles.AsNoTracking();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(r => r.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(r => r.Description.Contains(filter.Description));
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(r => r.Status == filter.Status.Value);
        }
        
        return new PageResultDto<List<RoleDto>>()
        {
            CollectionSize = await query.CountAsync(),
            Result = await query
                .Paginate(page, size)
                .ToRoleDto()
                .ToListAsync()
        };
    }
    
    
    public async Task CreateUpdateRole(RoleDto model)
    {
        var role = await _ctx.Roles.FirstOrDefaultAsync(r => r.RoleId == model.RoleId);

        if (role is null)
        {
            role = new Role();
            await _ctx.Roles.AddAsync(role);
        }
        
        role.Description = model.Description;
        role.Name = model.Name;
        role.Status = model.Status;
        
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteRoleById(int roleId)
    {
        var role = await _ctx.Roles
            .FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, TranslationStrings.ROLE_NOT_FOUND);
        }
        
        var activeUserRoles = await _ctx.Roles
            .AsNoTracking()
            .AnyAsync(r => r.RoleId == roleId);

        if (activeUserRoles)
        {
            throw new HttpException(HttpStatusCode.Forbidden, TranslationStrings.ROLE_USED_BY_USER);
        }
        
        _ctx.Roles.Remove(role);
        await _ctx.SaveChangesAsync();
    }
}