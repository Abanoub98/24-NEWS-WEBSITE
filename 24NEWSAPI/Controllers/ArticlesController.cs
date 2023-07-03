using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _24NEWSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _dBcontext;

        public ArticlesController(ApplicationDbContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var article = await _dBcontext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var article = await _dBcontext.Articles.OrderByDescending(x => x.Id).ToListAsync();

            return Ok(article);
        }



  


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ArticleDto dto)
        {
            //using var dataStream =new MemoryStream();
            //await dto.imgFile.CopyToAsync(dataStream);

            var article = new Article { 
            Title = dto.Title,
            AuthorName = dto.AuthorName,
            PublicationDate = dto.PublicationDate,
            CreationDate = DateTime.Now,
            AuthorID = dto.AuthorID,
            ArticleBody=dto.ArticleBody,
            BlockQoute=dto.BlockQoute,
            img=dto.img,
            };

            await _dBcontext.Articles.AddAsync(article);
            _dBcontext.SaveChanges();
            return Ok(article);
        }



        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ArticleDto dto)
        {
            var Article = await _dBcontext.Articles.FindAsync(id);

            if (Article == null)
            {
                return NotFound($"No Articles was found with ID: {id}");
            }

            Article.Title = dto.Title;
            Article.AuthorName = dto.AuthorName;
            Article.PublicationDate = dto.PublicationDate;
            Article.AuthorID = dto.AuthorID;
            Article.ArticleBody = dto.ArticleBody;
            Article.BlockQoute = dto.BlockQoute;
            Article.img = dto.img;
            Article.CreationDate = DateTime.Now;

            _dBcontext.SaveChanges();

            return Ok(Article);
        }






        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var article = await _dBcontext.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound($"No Article was found with ID: {id}");
            }

            _dBcontext.Articles.Remove(article);
            _dBcontext.SaveChanges();
            return Ok(article);
        }

    }
}
