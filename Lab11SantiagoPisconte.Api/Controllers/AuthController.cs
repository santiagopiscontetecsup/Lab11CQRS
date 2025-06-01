using Lab11SantiagoPisconte.Application.DTOs.Auth;
using Lab11SantiagoPisconte.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11SantiagoPisconte.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _mediator.Send(new LoginCommand { Username = dto.Username, Password = dto.Password });
        return StatusCode(result.StatusCode, new { token = result.Token, message = result.ErrorMessage });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        var result = await _mediator.Send(new RegisterCommand(dto));
        if (!result.IsSuccess)
            return BadRequest(new { message = result.ErrorMessage });
        return Ok();
    }
}