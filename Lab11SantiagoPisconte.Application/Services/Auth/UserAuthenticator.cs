using Lab11SantiagoPisconte.Api.Models;
using Lab11SantiagoPisconte.Domain.Interfaces.UnitOfWork;

namespace Lab11SantiagoPisconte.Application.Services.Auth;

public class UserAuthenticator
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAuthenticator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var allUsers = await _unitOfWork.Usuarios.GetAllAsync();
        var user = allUsers.FirstOrDefault(u => u.Username == username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }
}