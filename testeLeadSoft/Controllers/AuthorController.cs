using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Models;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly DataContext context;

		public AuthorController(DataContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Author>>> Get()
		{
			return Ok(await this.context.Authors.ToListAsync());
		}

		[HttpPost]
		public async Task<ActionResult<List<Author>>> AddAuthor(Author request)
		{
			var newAuthor = new Author
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Age = request.Age
			};

			this.context.Authors.Add(newAuthor);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Authors.ToListAsync());
		}
	}
}
