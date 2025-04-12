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
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerUser.Email);
            if (userExists != null)
                return BadRequest("Email already in use.");

            var user = new Users
            {
                Username = registerUser.Username,
                Email = registerUser.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if (userExists == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, userExists.Password))
                return Unauthorized("Invalid credentials");

            var token = new AuthService(_config).GenerateJwtToken(userExists);
            return Ok(new { token });
        }
    }
}
