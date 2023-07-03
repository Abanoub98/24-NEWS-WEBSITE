
namespace _24NEWSMVC.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string ArticleBody { get; set; }

        public DateTime CreationDate { get; set; }

 
        public DateTime PublicationDate { get; set; }

        public string? BlockQoute { get; set; }

        public byte[] img { get; set; }

        public IFormFile imgFile { get; set; }

        public int AuthorID { get; set; }

        public Dictionary<int, string> Authors { get; set; }
    }
}
