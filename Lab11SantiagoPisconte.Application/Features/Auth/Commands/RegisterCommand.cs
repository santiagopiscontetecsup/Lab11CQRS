using Lab11SantiagoPisconte.Application.DTOs.Auth;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Auth.Commands;

public record RegisterCommand : IRequest<AuthResultDto>
{
    public RegisterRequestDto RegisterDto { get; }

    public RegisterCommand(RegisterRequestDto dto)
    {
        RegisterDto = dto;
    }
}
