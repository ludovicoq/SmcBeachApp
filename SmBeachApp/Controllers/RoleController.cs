using Microsoft.AspNetCore.Mvc;
using SmBeachApp.Entities.Models;
using SmBeachApp.Entities.Models.Filters;
using SmBeachApp.Services;

namespace SmBeachApp.Controllers;

[ApiController]
[Route("api/role")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
    {
        var result = await _roleService.GetAllRolesAsync();
        return Ok(result);
    }

    [HttpGet("{roleId:int}")]
    public async Task<ActionResult<RoleDto>> GetRoleById(int roleId)
    {
        var result = await _roleService.GetRoleByIdAsync(roleId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RoleDto>> SearchRoles([FromBody] RoleFilterDto role, [FromQuery] int page,
        [FromQuery] int size)
    {
        var result = await _roleService.SearchRolesAsync(role, page, size);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<RoleDto>> UpdateRole([FromBody] RoleDto role)
    {
        await _roleService.CreateUpdateRole(role);
        return Ok(role);
    }

    [HttpDelete("{roleId:int}")]
    public async Task<ActionResult> DeleteRole(int roleId)
    {
        await _roleService.DeleteRoleById(roleId);
        return Ok();
    }
    
    
    
}