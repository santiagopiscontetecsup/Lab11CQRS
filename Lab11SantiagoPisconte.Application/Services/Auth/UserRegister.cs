using Lab11SantiagoPisconte.Api.Models;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;

namespace Lab11SantiagoPisconte.Application.Services.Auth;

public class UserRegister
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRegister(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool success, string errorMessage)> RegisterUserAsync(string username, string password, string email)
    {
        var allUsers = await _unitOfWork.Usuarios.GetAllAsync();
        if (allUsers.Any(u => u.Username == username))
            return (false, "El usuario ya existe.");

        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Email = email,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Usuarios.AddAsync(newUser);
        await _unitOfWork.SaveAsync();

        var role = (await _unitOfWork.Roles.GetAllAsync())
            .FirstOrDefault(r => r.RoleName == "Usuario");

        if (role != null)
        {
            await _unitOfWork.UserRoles.AddAsync(new UserRole
            {
                UserId = newUser.UserId,
                RoleId = role.RoleId,
                AssignedAt = DateTime.UtcNow
            });
            await _unitOfWork.SaveAsync();
        }

        return (true, null);
    }
}