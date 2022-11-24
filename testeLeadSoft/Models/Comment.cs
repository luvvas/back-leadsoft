using System.Text.Json.Serialization;

namespace testeLeadSoft.Models
{
	public class Comment
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		[JsonIgnore]
		public Article Article { get; set; }
		public Guid ArticleId { get; set; }
	}
}
