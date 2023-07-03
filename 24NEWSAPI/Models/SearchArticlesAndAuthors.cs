using Microsoft.Identity.Client;
using NuGet.Protocol.Core.Types;

namespace _24NEWSAPI.Models
{
    public class SearchArticlesAndAuthors
    {
        public List<Article> Articles { get; set; } 
        public List<Author> Author { get; set; }

    }
}
