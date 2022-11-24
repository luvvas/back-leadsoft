using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Migrations;
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
		public async Task<ActionResult<List<Author>>> AddAuthor(CreateAuthorDto request)
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

		[HttpGet("{AuthorId}")]
		public async Task<ActionResult<Author>> Get(Guid authorId)
		{
			var author = await this.context.Authors.FindAsync(authorId);
			if(author == null)
			{
				return BadRequest("Author not found");
			}

			return Ok(author);
		}

		[HttpPut]
		public async Task<ActionResult<List<Author>>> UpdateHero(CreateAuthorDto request)
		{
			var dbAuthor = await this.context.Authors.FindAsync(request.Id);
			if (dbAuthor == null)
			{
				return BadRequest("Author not found.");
			}

			dbAuthor.FirstName = request.FirstName;
			dbAuthor.LastName = request.LastName;
			dbAuthor.Age = request.Age;

			await this.context.SaveChangesAsync();

			return Ok(await this.context.Authors.ToListAsync());
		}

		[HttpDelete("{authorId}")]
		public async Task<ActionResult<List<Author>>> DeleteAuthor(Guid authorId)
		{
			var dbAuthor = await this.context.Authors.FindAsync(authorId);
			if (dbAuthor == null)
			{
				return BadRequest("Author not found.");
			}

			this.context.Authors.Remove(dbAuthor);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Authors.ToListAsync());
		}
	}
}
