using System.Net;
using Microsoft.EntityFrameworkCore;
using SmBeachApp.Data;
using SmBeachApp.Entities;
using SmBeachApp.Entities.Models;
using SmBeachApp.Entities.Models.Filters;
using SmBeachApp.Extensions.DbExtensions;
using SmBeachApp.Localization.Localization;

namespace SmBeachApp.Services;

public class GroupService
{
    private readonly SmBeachDbContext _ctx;

    public GroupService(SmBeachDbContext context)
    {
        _ctx = context;
    }

    public async Task<List<GroupDto>> GetAllGroupsAsync()
    {
        return await _ctx.Groups
            .AsNoTracking()
            .ToGroupDto()
            .ToListAsync();
    }

    public async Task<GroupDto> GetGroupByIdAsync(int groupId)
    {
        return await _ctx.Groups
            .AsNoTracking()
            .Where(g => g.GroupId == groupId)
            .ToGroupDto()
            .FirstOrDefaultAsync();
    }

    public async Task<PageResultDto<List<GroupDto>>> SearchGroupsAsync(GroupFilterDto filter, int page, int size)
    {
        var query = _ctx.Groups.AsNoTracking();

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
        
        return new PageResultDto<List<GroupDto>>()
        {
            CollectionSize = await query.CountAsync(),
            Result = await query
                .Paginate(page, size)
                .ToGroupDto()
                .ToListAsync()
        };
    }
    
    
    public async Task CreateUpdateGroupAsync(GroupDto model)
    {
        var group = await _ctx.Groups.FirstOrDefaultAsync(r => r.GroupId == model.GroupId);

        if (group is null)
        {
            group = new Group();
            await _ctx.Groups.AddAsync(group);
        }
        
        group.Description = model.Description;
        group.Name = model.Name;
        group.Status = model.Status;
        
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteGroupByIdAsync(int groupId)
    {
        var group = await _ctx.Groups
            .FirstOrDefaultAsync(r => r.GroupId == groupId);

        if (group is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, TranslationStrings.GROUP_NOT_FOUND);
        }
        
        var activeUserGroups = await _ctx.UserGroups
            .AsNoTracking()
            .AnyAsync(r => r.GroupId == groupId);

        if (activeUserGroups)
        {
            throw new HttpException(HttpStatusCode.Forbidden, TranslationStrings.GROUP_USED_BY_USER);
        }
        
        _ctx.Groups.Remove(group);
        await _ctx.SaveChangesAsync();
    }
}