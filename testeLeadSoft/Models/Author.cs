using System.Text.Json.Serialization;

namespace testeLeadSoft.Models
{
	public class Author
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		[JsonIgnore]
		public List<Article> Articles { get; set; }
	}
}
