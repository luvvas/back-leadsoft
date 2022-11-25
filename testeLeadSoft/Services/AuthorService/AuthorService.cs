using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		public Task<ActionResult<List<Author>>> DeleteAuthor(Guid authorId)
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<List<Author>>> Get()
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<Author>> Get(Guid authorId)
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<List<Author>>> UpdateHero(CreateAuthorDto request)
		{
			throw new NotImplementedException();
		}
	}
}
