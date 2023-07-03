namespace _24NEWSAPI.Dtos
{
    public class ArticleDto
    {
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string ArticleBody { get; set; }


        [PDateValidationAttribute(7, ErrorMessage = "Publication Date must be Today or a Week from Today")]
        public DateTime PublicationDate { get; set; }

        public string? BlockQoute { get; set; }

        public byte[] img { get; set; }

        public int AuthorID { get; set; }

    }
}
