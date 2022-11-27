﻿using AutoMapper;
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
				var authors = await context.Authors.ToListAsync();
				serviceResponse.Data = authors.Select(a => mapper.Map<GetAuthorDto>(a)).ToList();
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
