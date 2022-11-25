using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.AuthorService
{
	public class AuthorService : IAuthorService
	{
		private readonly DataContext context;

		public AuthorService(DataContext context) 
		{
			this.context = context;
		}

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

			return await this.context.Authors.ToListAsync();
		}

		public async Task<ActionResult<List<Author>>> DeleteAuthor(Guid authorId)
		{
			var dbAuthor = await this.context.Authors.FindAsync(authorId);
			if (dbAuthor == null)
			{
				// return BadRequest("Author not found");
				return null;
			}

			this.context.Authors.Remove(dbAuthor);
			await this.context.SaveChangesAsync();

			return await this.context.Authors.ToListAsync();
		}

		public async Task<ActionResult<List<Author>>> Get()
		{
			return await this.context.Authors.ToListAsync();
		}

		public async Task<ActionResult<Author>> Get(Guid authorId)
		{
			var author = await this.context.Authors.FindAsync(authorId);
			if (author == null)
			{
				// BadRequest("Author not found");
				return null;
			}

			return author;
		}

		public async Task<ActionResult<List<Author>>> UpdateAuthor(CreateAuthorDto request)
		{
			var dbAuthor = await this.context.Authors.FindAsync(request.Id);
			if (dbAuthor == null)
			{
				// BadRequest("Author not found.")
				return null;
			}

			dbAuthor.FirstName = request.FirstName;
			dbAuthor.LastName = request.LastName;
			dbAuthor.Age = request.Age;

			await this.context.SaveChangesAsync();

			return await this.context.Authors.ToListAsync();
		}
	}
}
