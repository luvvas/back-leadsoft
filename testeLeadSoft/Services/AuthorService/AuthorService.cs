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

		public async Task<ServiceResponse<List<Author>>> AddAuthor(CreateAuthorDto request)
		{
			var serviceResponse = new ServiceResponse<List<Author>>();

			var newAuthor = new Author
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Age = request.Age
			};

			this.context.Authors.Add(newAuthor);
			await this.context.SaveChangesAsync();

			serviceResponse.Data = await this.context.Authors.ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Author>>> DeleteAuthor(Guid authorId)
		{
			var serviceResponse = new ServiceResponse<List<Author>>();

			var dbAuthor = await this.context.Authors.FindAsync(authorId);
			if (dbAuthor == null)
			{
				// return BadRequest("Author not found");
				return null;
			}

			this.context.Authors.Remove(dbAuthor);
			await this.context.SaveChangesAsync();

			serviceResponse.Data = await this.context.Authors.ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Author>>> Get()
		{
			var serviceResponse = new ServiceResponse<List<Author>>();
			serviceResponse.Data = await this.context.Authors.ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<Author>> Get(Guid authorId)
		{
			var serviceResponse = new ServiceResponse<Author>();

			try
			{
				var author = await this.context.Authors.FindAsync(authorId);
				if (author != null)
				{
					serviceResponse.Data = author;
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Author not found.";
				}
			} catch(Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Author>>> UpdateAuthor(CreateAuthorDto request)
		{
			var serviceResponse = new ServiceResponse<List<Author>>();

			try
			{
				var dbAuthor = await this.context.Authors.FindAsync(request.Id);
				if (dbAuthor != null)
				{
					dbAuthor.FirstName = request.FirstName;
					dbAuthor.LastName = request.LastName;
					dbAuthor.Age = request.Age;

					await this.context.SaveChangesAsync();

					serviceResponse.Data = await this.context.Authors.ToListAsync();
				} else {
					serviceResponse.Success = false;
					serviceResponse.Message = "Author not found.";
				}
			} catch(Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}
	}
}
