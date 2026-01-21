using backend.Data;
using backend.DTOs;
using backend.DTOs.Auth;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _context.Users
                .AnyAsync(u => u.UserName == dto.UserName);

            if (exists)
                return BadRequest("Usuário já existe.");

            var person = new Person
            {
                Name = dto.Name,
                Email = dto.Email
            };

            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Person = person
            };

            _context.Persons.Add(person);
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("Registrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!valid)
                return Unauthorized("Usuário ou senha inválidos.");

            return Ok(new
            {
                message = "Login OK",
                userId = user.Id,
                personId = user.PersonId,
                name = user.Person?.Name
            });
        }
    }
}
