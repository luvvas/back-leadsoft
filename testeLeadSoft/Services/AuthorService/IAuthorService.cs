using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.AuthorService
{
	public interface IAuthorService
	{
		Task<List<Author>> Get();
		Task<List<Author>> AddAuthor(CreateAuthorDto request);
		Task<Author> Get(Guid authorId);
		Task<List<Author>> UpdateAuthor(CreateAuthorDto request);
		Task<List<Author>> DeleteAuthor(Guid authorId);
	}
}