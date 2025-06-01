namespace Lab11SantiagoPisconte.Application.DTOs.Auth;

public class AuthResultDto
{
    public bool IsSuccess { get; set; }
    public string? Token { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }
}