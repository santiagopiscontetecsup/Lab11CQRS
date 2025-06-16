using Lab11SantiagoPisconte.Api.Models;
using Lab11SantiagoPisconte.Domain.Interfaces.Repositories;

namespace Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IGenericRepository<User, Guid> Usuarios { get; }
    IGenericRepository<Role, Guid> Roles { get; }
    IGenericRepository<UserRole, Guid> UserRoles { get; }
    IGenericRepository<Ticket, Guid> Tickets { get; }
    IGenericRepository<Response, Guid> Responses { get; }

    Task<int> SaveAsync();
}