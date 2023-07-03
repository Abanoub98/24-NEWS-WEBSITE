using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _24NEWSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _dBcontext;

        public SearchController(ApplicationDbContext dBcontext)
        {
            _dBcontext = dBcontext;
        }




        [HttpGet("{searchQuery}")]
        public async Task<IActionResult> SearchArticles(string searchQuery)
        {

            if (_dBcontext.Articles == null && _dBcontext.Authors ==null )
            {
                return Problem("No Articles or Authors was found");
            }

            SearchArticlesAndAuthors returnedResult = new SearchArticlesAndAuthors();

            var returnedArticles = from ar in _dBcontext.Articles
                                   select ar;
            var returnedAuthors = from au in _dBcontext.Authors
                                   select au;


            if (!String.IsNullOrEmpty(searchQuery))
            {
                
                if(_dBcontext.Articles!=null)
                {
                    returnedArticles = _dBcontext.Articles.Where(x => x.Title.Contains(searchQuery)).OrderByDescending(x => x.Id);
                }
                if(_dBcontext.Authors!=null)
                {
                    returnedAuthors = _dBcontext.Authors.Where(x => x.Name.Contains(searchQuery)).OrderByDescending(x => x.Id);
                }
                                  
            }
            returnedResult.Author = await returnedAuthors.ToListAsync();
            returnedResult.Articles = await returnedArticles.ToListAsync();

            return Ok(returnedResult);
        }


    }
}
