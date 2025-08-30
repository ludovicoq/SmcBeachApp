using Microsoft.AspNetCore.Mvc;
using SmBeachApp.Entities.Models;
using SmBeachApp.Entities.Models.Filters;
using SmBeachApp.Services;

namespace SmBeachApp.Controllers;

[ApiController]
[Route("api/group")]
public class GroupController : ControllerBase
{
    private readonly GroupService _groupservice;

    public GroupController(GroupService groupService)
    {
        _groupservice = groupService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GroupDto>>> GetAllGroups()
    {
        var result = await _groupservice.GetAllGroupsAsync();
        return Ok(result);
    }

    [HttpGet("{groupId:int}")]
    public async Task<ActionResult> GetRoleById(int groupId)
    {
        var result = await _groupservice.GetGroupByIdAsync(groupId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GroupDto>> SearchGroups([FromBody] GroupFilterDto filter, [FromQuery] int page,
        [FromQuery] int size)
    {
        var result = await _groupservice.SearchGroupsAsync(filter, page, size);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateRole([FromBody] GroupDto group)
    {
        await _groupservice.CreateUpdateGroupAsync(group);
        return Ok();
    }

    [HttpDelete("{groupId:int}")]
    public async Task<ActionResult> DeleteRole(int groupId)
    {
        await _groupservice.DeleteGroupByIdAsync(groupId);
        return Ok();
    }
    
    
    
}