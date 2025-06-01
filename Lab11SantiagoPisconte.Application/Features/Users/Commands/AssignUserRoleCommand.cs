using MediatR;

namespace Lab11SantiagoPisconte.Application.Features.Users.Commands;

public record AssignUserRoleCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public AssignUserRoleCommand(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}