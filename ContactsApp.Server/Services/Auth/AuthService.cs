using AutoMapper;
using ContactsApp.Server.Dtos.Users;
using ContactsApp.Server.Models;
using ContactsApp.Server.Repositories.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactsApp.Server.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="AuthService"/> z repozytorium użytkowników, konfiguracją i mapperem.
        /// </summary>
        /// <param name="userRepository">Repozytorium użytkowników.</param>
        /// <param name="config">Reprezentacja konfiguracji aplikacji.</param>
        /// <param name="mapper">Mapper do mapowania obiektów.</param>
        public AuthService(IUsersRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        /// <summary>
        /// Rejestruje nowego użytkownika w systemie.
        /// </summary>
        /// <param name="registerDto">Szczegółowe dane kontaktu w formacie <see cref="RegisterUserDto"/></param>
        /// <returns>
        /// Krotka (bool, string?):
        /// <list type="bullet">
        /// <item><description><c>IsSuccess</c> - true, jeśli rejestracja zakończyła się sukcesem; false, jeśli wystąpił błąd</description></item>
        /// <item><description><c>ErrorMessage</c> - komunikat błędu, jeśli wystąpił; null w przypadku sukcesu</description></item>
        /// </list>
        /// </returns>
        public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterUserDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return (false, "Email already in use.");

            var user = _mapper.Map<Users>(registerDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, null);
        }
        /// <summary>
        /// Loguje użytkownika do systemu i generuje token JWT.
        /// </summary>
        /// <param name="loginDto">Dane do logowania kontaktu w formacie <see cref="LoginUserDto"/></param>
        /// <returns>
        /// Krotka (string? Token, string? ErrorMessage, int? userId):
        /// <list type="bullet">
        /// <item><description><c>Token</c> - wygenerowany token JWT, jeśli logowanie się powiodło; null w przypadku błędu</description></item>
        /// <item><description><c>ErrorMessage</c> - komunikat błędu, jeśli logowanie się nie powiodło; null w przypadku sukcesu</description></item>
        /// <item><description><c>userId</c> - ID zalogowanego użytkownika, jeśli logowanie się powiodło; null w przypadku błędu</description></item>
        /// </list>
        /// </returns>
        public async Task<(string? Token, string? ErrorMessage, int? userId)> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return (null, "Invalid credentials", null);

            var token = GenerateJwtToken(user);
            return (token, null, user.Id);
        }
        /// <summary>
        /// Generuje token JWT dla użytkownika.
        /// </summary>
        /// <param name="user">Użytkownik w formacie <see cref="Users"/></param>
        /// <returns>
        /// Wygenerowany token JWT jako <see cref="string"/>
        /// </returns>
        public string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(_config.GetValue<int>("Jwt:ExpireTime")),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
