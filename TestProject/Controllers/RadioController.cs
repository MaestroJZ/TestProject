using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Models.Dto;
using TestProject.Repository.IRepository;

namespace TestProject.Controllers
{
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("items/add")]
        [HttpPost("NewProduct")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var category = await _dbContext.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                return BadRequest("The category does not exist.");
            }

            //extra.CategoryId = category.Id;
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}
