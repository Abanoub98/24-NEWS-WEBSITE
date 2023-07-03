using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _24NEWSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dBcontext; 

        public AuthorsController(ApplicationDbContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var authors = await _dBcontext.Authors.OrderByDescending(x=> x.Id).ToListAsync();

            return Ok(authors);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var author = await _dBcontext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AuthorDto dto)
        {

            var author = new Author { Name = dto.Name ,Email = dto.Email , Bio = dto.Bio };

            await _dBcontext.Authors.AddAsync(author);
            _dBcontext.SaveChanges();
            return Ok(author);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AuthorDto dto)
        {
            var author = await _dBcontext.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound($"No Author was found with ID: {id}");
            }


            author.Name = dto.Name;
            author.Email = dto.Email;
            author.Bio = dto.Bio;

           _dBcontext.SaveChanges();

            return Ok(author);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var author = await _dBcontext.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound($"No Author was found with ID: {id}");
            }

            _dBcontext.Authors.Remove(author);
            _dBcontext.SaveChanges();
            return Ok(author);
        }
    }
}
