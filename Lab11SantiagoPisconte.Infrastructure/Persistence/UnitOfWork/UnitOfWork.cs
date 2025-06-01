using Lab11SantiagoPisconte.Api.Models;
using Lab11SantiagoPisconte.Domain.Interfaces.Repositories;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;
using Lab11SantiagoPisconte.Infrastructure.Persistence.Repositories;

namespace Lab11SantiagoPisconte.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly PisconteTicketSystemContext _context;
    
    public IGenericRepository<User, Guid> Usuarios { get; }
    public IGenericRepository<Role, Guid> Roles { get; }
    public IGenericRepository<UserRole, Guid> UserRoles { get; }
    public IGenericRepository<Ticket, Guid> Tickets { get; }

    public UnitOfWork(PisconteTicketSystemContext context)
    {
        _context = context;
        Usuarios = new GenericRepository<User, Guid>(_context);
        Roles = new GenericRepository<Role, Guid>(_context);
        UserRoles = new GenericRepository<UserRole, Guid>(_context);
        Tickets = new GenericRepository<Ticket, Guid>(_context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}