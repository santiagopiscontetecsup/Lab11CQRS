using Lab11SantiagoPisconte.Application.DTOs.Auth;
using Lab11SantiagoPisconte.Application.Features.Auth.Commands;
using Lab11SantiagoPisconte.Application.Services.Auth;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Auth.Handlers;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResultDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResultDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var dto = request.RegisterDto;

        var registrar = new UserRegister(_unitOfWork);
        var (success, errorMessage) = await registrar.RegisterUserAsync(dto.Username, dto.Password, dto.Email);

        if (!success)
        {
            return new AuthResultDto
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        return new AuthResultDto
        {
            IsSuccess = true
        };
    }
}