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

		/// <summary>
		/// Get all authors on Database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<List<Author>>> Get()
		{
			return Ok(await this.context.Authors.ToListAsync());
		}

		/// <summary>
		/// Include a Author on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
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
		
		/// <summary>
		/// Get a author by it's id
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpGet("{authorId}")]
		public async Task<ActionResult<Author>> Get(Guid authorId)
		{
			var author = await this.context.Authors.FindAsync(authorId);
			if(author == null)
			{
				return BadRequest("Author not found");
			}

			return Ok(author);
		}

		/// <summary>
		/// Update a author on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Delete a author on Database
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
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
