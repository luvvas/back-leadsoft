using System.Text.Json.Serialization;

namespace testeLeadSoft.Models
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		[JsonIgnore]
		public List<Article> Articles { get; set; }
	}
}