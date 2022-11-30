using AutoMapper;
using Microsoft.EntityFrameworkCore;

using testeLeadSoft.Data;
using testeLeadSoft.Dto.Author;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.AuthorService
{
    public class AuthorService : IAuthorService
	{
		private readonly DataContext context;
		private readonly IMapper mapper;

		public AuthorService(DataContext context, IMapper mapper) 
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetAuthorDto>>> AddAuthor(CreateAuthorDto request)
		{
			var serviceResponse = new ServiceResponse<List<GetAuthorDto>>();

			try 
			{
				var newAuthor = new Author
				{
					FirstName = request.FirstName,
					LastName = request.LastName,
					Age = request.Age
				};

				context.Authors.Add(mapper.Map<Author>(newAuthor));
				await context.SaveChangesAsync();

				serviceResponse.Data = await context.Authors.Select(a => mapper.Map<GetAuthorDto>(a)).ToListAsync();
				serviceResponse.Message = "Author successfully created.";
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetAuthorDto>>> DeleteAuthor(Guid authorId)
		{
			var serviceResponse = new ServiceResponse<List<GetAuthorDto>>();

			try
			{
				var dbAuthor = await context.Authors.FindAsync(authorId);
				if (dbAuthor != null)
				{
					context.Authors.Remove(dbAuthor);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Authors.Select(a => mapper.Map<GetAuthorDto>(a)).ToListAsync();
					serviceResponse.Message = "Author successfully deleted.";
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

		public async Task<ServiceResponse<List<GetAuthorDto>>> Get()
		{
			var serviceResponse = new ServiceResponse<List<GetAuthorDto>>();

			try
			{
				// FORTE
				var dbAuthors = await context.Authors.Include(a => a.Articles).ToListAsync();

				serviceResponse.Data = dbAuthors.Select(a => mapper.Map<GetAuthorDto>(a)).ToList();
				serviceResponse.Message = "All Authors successfully listed.";
			} catch(Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetAuthorDto>> Get(Guid authorId)
		{
			var serviceResponse = new ServiceResponse<GetAuthorDto>();

			try
			{
				var author = await context.Authors.FindAsync(authorId);
				if (author != null)
				{
					serviceResponse.Data = mapper.Map<GetAuthorDto>(author);
					serviceResponse.Message = "Author successfully listed.";
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Author listed.";
				}
			} catch(Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(GetAuthorDto request)
		{
			var serviceResponse = new ServiceResponse<GetAuthorDto>();

			try
			{
				var dbAuthor = await context.Authors.FindAsync(request.Id);
				if (dbAuthor != null)
				{
					dbAuthor.FirstName = request.FirstName;
					dbAuthor.LastName = request.LastName;
					dbAuthor.Age = request.Age;

					await context.SaveChangesAsync();

					serviceResponse.Data = mapper.Map<GetAuthorDto>(dbAuthor);
					serviceResponse.Message = "Author successfully Updated.";
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
