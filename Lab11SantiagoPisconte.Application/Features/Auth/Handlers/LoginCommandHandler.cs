using Lab11SantiagoPisconte.Application.DTOs.Auth;
using Lab11SantiagoPisconte.Application.Features.Auth.Commands;
using Lab11SantiagoPisconte.Application.Services.Auth;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Lab11SantiagoPisconte.Application.Features.Auth.Handlers;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenGenerator _tokenGenerator;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _tokenGenerator = new JwtTokenGenerator(configuration);
    }

    public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var authenticator = new UserAuthenticator(_unitOfWork);
        var user = await authenticator.AuthenticateAsync(request.Username, request.Password);
        if (user == null)
            return new AuthResultDto { IsSuccess = false, ErrorMessage = "Usuario o contraseña inválidos.", StatusCode = 401 };

        var userRoles = await _unitOfWork.UserRoles.GetAllAsync();
        var roles = await _unitOfWork.Roles.GetAllAsync();

        var roleNames = (
            from ur in userRoles
            join r in roles on ur.RoleId equals r.RoleId
            where ur.UserId == user.UserId
            select r.RoleName
        ).ToList();

        var token = _tokenGenerator.GenerateToken(user, roleNames);

        return new AuthResultDto { IsSuccess = true, Token = token, StatusCode = 200 };
    }
}