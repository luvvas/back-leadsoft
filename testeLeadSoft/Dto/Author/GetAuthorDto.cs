using testeLeadSoft.Models;

namespace testeLeadSoft.Dto.Author
{
	public class GetAuthorDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public List<Models.Article> Articles { get; set; }
	}
}
