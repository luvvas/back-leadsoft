using System.Text.Json.Serialization;

using testeLeadSoft.Models;

namespace testeLeadSoft.Dto
{
	public class CreateArticleDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Text { get; set; }
		public Guid AuthorId { get; set; }
		public Guid CategoryId { get; set; }
	}
}
