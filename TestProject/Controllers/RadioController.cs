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
    //[Authorize]
    [Route("api/data/")]
    public class RadioController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public RadioController(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        [HttpGet("items")]
        public async Task<IActionResult> GetAllItems()
        {
            var radios = await _context.Radios.ToListAsync();
            return Ok(radios);
        }

        [HttpPost("items/add")]
        public async Task<ActionResult<Radio>> PostRadio(Radio radio)
        {
            var existingRadio = await _context.Radios.FindAsync(radio.Id);

            if (existingRadio != null)
            {
                return Conflict("Радио уже существует.");
            }
            else
            {
                _context.Radios.Add(radio);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
