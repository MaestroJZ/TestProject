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
            var radios = _context.Radios.Where(radio => radio.IsDeleted).ToList(); ;
            return Ok(radios);
        }

        [HttpPost("items/add")]
        public async Task<ActionResult<Radio>> PostRadio(Radio radio)
        {
            var existingRadio = await _context.Radios.FindAsync(radio.NumberRadio);

            if (existingRadio != null)
            {
                return Conflict("Радио уже существует.");
            }
            else
            {
                _context.Radios.Add(radio);
                await _context.SaveChangesAsync();
                return Ok("Добавлено");
            }
        }
        [HttpDelete("items/delete/{id}")]
        public async Task<ActionResult> DeleteRadio(int id)
        {
            var radioToDelete = await _context.Radios.FindAsync(id);

            if (radioToDelete == null)
            {
                return NotFound("Радио не найдено.");
            }
            else
            {                
                radioToDelete.IsDeleted = false;
                await _context.SaveChangesAsync();
                return Ok("Удалено");
            }
        }
        [HttpPut("items/editItem/{id}")]
        public async Task<ActionResult> UpdateRadio(int id,Radio updatedRadio)
        {
            var existingRadio = await _context.Radios.FindAsync(id);   
            existingRadio.NumberRadio = updatedRadio.NumberRadio;
            existingRadio.Mine = updatedRadio.Mine;
            existingRadio.WorkerName = updatedRadio.WorkerName;
            existingRadio.UserId = updatedRadio.UserId;
            existingRadio.TableNmbrAndPlace = updatedRadio.TableNmbrAndPlace;
            existingRadio.RadioStatus = updatedRadio.RadioStatus;
            await _context.SaveChangesAsync();
            return Ok("Изменено");
            
        }
    }
}
