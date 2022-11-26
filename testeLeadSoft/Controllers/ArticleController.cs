using Microsoft.AspNetCore.Mvc;

using testeLeadSoft.Dto;
using testeLeadSoft.Models;
using testeLeadSoft.Services.ArticleService;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticleController : ControllerBase
	{
		private readonly IArticleService articleService;
		public ArticleController(IArticleService articleService) 
		{
			this.articleService = articleService;
		}

		/// <summary>
		/// Get a article by it's id
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpGet("{authorId}")]
		public async Task<ActionResult<ServiceResponse<List<Article>>>> Get(Guid authorId)
		{
			var response = await this.articleService.Get(authorId);
			if(response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Include a article on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<Article>>>> AddArticle(CreateArticleDto request)
		{
			var response = await this.articleService.AddArticle(request);
			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Update a article on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<ServiceResponse<List<Article>>>> UpdateArticle(CreateArticleDto request)
		{
			var response = await this.articleService.UpdateArticle(request);
			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Delete a article on Database
		/// </summary>
		/// <param name="articleId"></param>
		/// <returns></returns>
		[HttpDelete("{articleId}")]
		public async Task<ActionResult<ServiceResponse<List<Article>>>> DeleteArticle(Guid articleId)
		{
			var response = await this.articleService.DeleteArticle(articleId);
			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}
	}
}
