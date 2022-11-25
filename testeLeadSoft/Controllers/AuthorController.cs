using Microsoft.AspNetCore.Mvc;

using testeLeadSoft.Dto;
using testeLeadSoft.Models;
using testeLeadSoft.Services.AuthorService;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService authorService;

		public AuthorController(IAuthorService authorService)
		{
			this.authorService = authorService;
		}

		/// <summary>
		/// Get all authors on Database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<List<Author>>> Get()
		{
			return Ok(await this.authorService.Get());
		}

		/// <summary>
		/// Include a Author on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<List<Author>>> AddAuthor(CreateAuthorDto request)
		{
			return Ok(await this.authorService.AddAuthor(request));
		}
		
		/// <summary>
		/// Get a author by it's id
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpGet("{authorId}")]
		public async Task<ActionResult<Author>> Get(Guid authorId)
		{
			return Ok(await this.authorService.Get(authorId));
		}

		/// <summary>
		/// Update a author on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<List<Author>>> UpdateAuthor(CreateAuthorDto request)
		{
			return Ok(await this.authorService.UpdateAuthor(request));
		}

		/// <summary>
		/// Delete a author on Database
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpDelete("{authorId}")]
		public async Task<ActionResult<List<Author>>> DeleteAuthor(Guid authorId)
		{
			//if(await this.authorService.DeleteAuthor(authorId) == null)
			//{
			//	return BadRequest("Author not found.");
			//}

			return Ok(await this.authorService.DeleteAuthor(authorId));
		}
	}
}
