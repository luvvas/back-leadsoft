namespace testeLeadSoft.Dto.Comment
{
	public class GetCommentDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public Guid ArticleId { get; set; }
	}
}
