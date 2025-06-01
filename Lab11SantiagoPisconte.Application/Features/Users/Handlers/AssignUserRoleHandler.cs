using Lab11SantiagoPisconte.Api.Models;
using Lab11SantiagoPisconte.Application.Features.Users.Commands;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Users.Handlers;

internal class AssignUserRoleHandler : IRequestHandler<AssignUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Usuarios.GetByIdAsync(request.UserId);
        var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId);

        if (user == null || role == null)
            return false;

        var existing = (await _unitOfWork.UserRoles.GetAllAsync())
            .FirstOrDefault(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId);

        if (existing != null) return false;

        var userRole = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = DateTime.UtcNow
        };

        await _unitOfWork.UserRoles.AddAsync(userRole);
        await _unitOfWork.SaveAsync();

        return true;
    }
}