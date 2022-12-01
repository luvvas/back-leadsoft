using testeLeadSoft.Models;

namespace testeLeadSoft.Dto.Category
{
	public class GetCategoryDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public List<Models.Article> Articles { get; set; }
	}
}
