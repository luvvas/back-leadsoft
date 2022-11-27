using Microsoft.AspNetCore.Mvc;

using testeLeadSoft.Dto.Author;
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
		public async Task<ActionResult<ServiceResponse<List<GetAuthorDto>>>> Get()
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
		public async Task<ActionResult<ServiceResponse<List<GetAuthorDto>>>> AddAuthor([FromBody]CreateAuthorDto request)
		{
			var response = await authorService.AddAuthor(request);
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
		[HttpGet("{authorId:guid}")]
		public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> Get([FromRoute]Guid authorId)
		{
			var response = await authorService.Get(authorId);
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
		public async Task<ActionResult<ServiceResponse<List<GetAuthorDto>>>> UpdateAuthor([FromBody]GetAuthorDto request)
		{
			var response = await authorService.UpdateAuthor(request);
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
		[HttpDelete("{authorId:guid}")]
		public async Task<ActionResult<ServiceResponse<List<GetAuthorDto>>>> DeleteAuthor([FromRoute]Guid authorId)
		{
			var response = await authorService.DeleteAuthor(authorId);
			if(response.Data == null)
			{
				return NotFound(response);
			}
			
			return Ok(response);
		}
	}
}
