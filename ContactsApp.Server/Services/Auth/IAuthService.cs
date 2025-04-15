using ContactsApp.Server.Dtos.Users;

namespace ContactsApp.Server.Services.Auth
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterUserDto registerDto);
        Task<(string? Token, string? ErrorMessage, int? userId)> LoginAsync(LoginUserDto loginDto);
        string GenerateJwtToken(Models.Users user);
    }
}
