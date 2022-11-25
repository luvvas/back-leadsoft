using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.AuthorService
{
	public interface IAuthorService
	{
		Task<ActionResult<List<Author>>> Get();
		Task<ActionResult<List<Author>>> AddAuthor(CreateAuthorDto request);
		Task<ActionResult<Author>> Get(Guid authorId);
		Task<ActionResult<List<Author>>> UpdateAuthor(CreateAuthorDto request);
		Task<ActionResult<List<Author>>> DeleteAuthor(Guid authorId);
	}
}