using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.AuthorService
{
	public interface IAuthorService
	{
		Task<ServiceResponse<List<Author>>> Get();
		Task<ServiceResponse<List<Author>>> AddAuthor(CreateAuthorDto request);
		Task<ServiceResponse<Author>> Get(Guid authorId);
		Task<ServiceResponse<List<Author>>> UpdateAuthor(CreateAuthorDto request);
		Task<ServiceResponse<List<Author>>> DeleteAuthor(Guid authorId);
	}
}