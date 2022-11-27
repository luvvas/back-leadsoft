using System.Text.Json.Serialization;
using testeLeadSoft.Models;

namespace testeLeadSoft.Dto
{
	public class CreateCommentDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public Guid ArticleId { get; set; }
	}
}
