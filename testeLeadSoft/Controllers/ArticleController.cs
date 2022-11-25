using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<ActionResult<List<Article>>> Get(Guid authorId)
		{
			return Ok(await this.articleService.Get(authorId));
		}

		/// <summary>
		/// Include a article on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<List<Article>>> AddArticle(CreateArticleDto request)
		{
			return Ok(await this.articleService.AddArticle(request));
		}

		/// <summary>
		/// Update a article on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<List<Article>>> UpdateArticle(CreateArticleDto request)
		{
			return Ok(await this.articleService.UpdateArticle(request));
		}

		/// <summary>
		/// Delete a article on Database
		/// </summary>
		/// <param name="articleId"></param>
		/// <returns></returns>
		[HttpDelete("{articleId}")]
		public async Task<ActionResult<List<Article>>> DeleteArticle(Guid articleId)
		{
			return Ok(await this.articleService.DeleteArticle(articleId));
		}
	}
}
