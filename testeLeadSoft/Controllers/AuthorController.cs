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
		public async Task<ActionResult<ServiceResponse<List<Author>>>> Get()
		{
			var response = await authorService.Get();
			if(response.Data == null)
			{
				return NotFound(response);
			}
			
			return Ok(response);
		}

		/// <summary>
		/// Include a Author on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> AddAuthor(CreateAuthorDto request)
		{
			var response = await this.authorService.AddAuthor(request);
			if(response.Data == null)
			{
				return NotFound(response);
			}
			
			return Ok(response);
		}
		
		/// <summary>
		/// Get a author by it's id
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpGet("{authorId}")]
		public async Task<ActionResult<ServiceResponse<Author>>> Get(Guid authorId)
		{
			var response = await this.authorService.Get(authorId);
			if(response.Data == null)
			{
				return NotFound(response);
			}
			
			return Ok(response);
		}

		/// <summary>
		/// Update a author on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> UpdateAuthor(CreateAuthorDto request)
		{
			var response = await this.authorService.UpdateAuthor(request);
			if(response.Data == null)
			{
				return NotFound(response);
			}
			
			return Ok(response);
		}

		/// <summary>
		/// Delete a author on Database
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpDelete("{authorId}")]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> DeleteAuthor(Guid authorId)
		{
			var response = await this.authorService.DeleteAuthor(authorId);
			if(response.Data == null)
			{
				return NotFound(response);
			}
			
			return Ok(response);
		}
	}
}
