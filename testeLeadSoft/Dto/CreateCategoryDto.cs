using System.Text.Json.Serialization;

namespace testeLeadSoft.Dto
{
	public class CreateCategoryDto
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
	}
}
