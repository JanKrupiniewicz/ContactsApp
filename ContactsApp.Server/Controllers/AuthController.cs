using ContactsApp.Server.Data;
using ContactsApp.Server.Dtos.Users;
using ContactsApp.Server.Models;
using ContactsApp.Server.Services.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System;

namespace ContactsApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="AuthController"/> z usługą autoryzacji.
        /// </summary>
        /// <param name="authService">Serwis autoryzacji.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Rejestruje nowego użytkownika w systemie.
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns>Status operacji rejestracji.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            var result = await _authService.RegisterAsync(registerUser);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok("User registered successfully");
        }
        /// <summary>
        /// Loguje użytkownika do systemu i generuje token JWT.
        /// </summary>
        /// <param name="loginUser">Dane logowania użytkownika.</param>
        /// <returns>Token JWT i identyfikator użytkownika, jeśli logowanie zakończyło się sukcesem.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            var result = await _authService.LoginAsync(loginUser);
            if (result.Token == null)
                return Unauthorized(result.ErrorMessage);

            return Ok(
                new
                {
                    Token = result.Token,
                    UserId = result.userId
                }
            );
        }
    }
}
