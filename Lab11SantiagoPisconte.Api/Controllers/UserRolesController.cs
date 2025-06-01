using Lab11SantiagoPisconte.Application.DTOs.Users;
using Lab11SantiagoPisconte.Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11SantiagoPisconte.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
    {
        var result = await _mediator.Send(new AssignUserRoleCommand(dto.UserId, dto.RoleId));
        return Ok(result);
    }
}