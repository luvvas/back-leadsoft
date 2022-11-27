namespace testeLeadSoft.Dto.Article
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; } 
        public Guid? CategoryId { get; set; } = Guid.Empty;
    }
}
