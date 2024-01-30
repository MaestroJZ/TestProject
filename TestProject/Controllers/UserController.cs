using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Models;
using TestProject.Models.Dto;
using TestProject.Repository.IRepository;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public UserController(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (string.IsNullOrEmpty(loginResponse.Token))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(loginResponse);
        }
        //[HttpGet("users")]
        //[Authorize]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var users = await _context.Users.ToListAsync();
        //    return Ok(users);
        //}
    }
}
