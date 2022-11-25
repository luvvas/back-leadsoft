using System.Text.Json.Serialization;

using testeLeadSoft.Models;

namespace testeLeadSoft.Dto
{
	public class CreateAuthorDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
	}
}
