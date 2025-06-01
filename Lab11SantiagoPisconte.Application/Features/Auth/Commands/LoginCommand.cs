using Lab11SantiagoPisconte.Application.DTOs.Auth;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Auth.Commands;

public record LoginCommand : IRequest<AuthResultDto>
{
    public string Username;
    public string Password;
}
