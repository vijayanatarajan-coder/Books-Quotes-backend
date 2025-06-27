using Microsoft.AspNetCore.Mvc;
using BackendApi.Models;
using BackendApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("All fields are required.");
            }

           
            var existingUser = await _context.UserRegistrations
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return Conflict("A user with this email already exists.");
            }
              string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
              user.Password = hashedPassword;
              
            _context.UserRegistrations.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.UserRegistrations.ToListAsync();
            return Ok(users);
        }
    }
}
