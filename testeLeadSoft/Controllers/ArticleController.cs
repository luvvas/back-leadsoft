﻿using Microsoft.AspNetCore.Mvc;

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
		/// Get all article on Database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Article>>>> Get()
		{
			var response = await articleService.Get();
			if(response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Get a article by it's id
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpGet("{articleId:guid}")]
		public async Task<ActionResult<ServiceResponse<Article>>> Get([FromRoute]Guid articleId)
		{
			var response = await articleService.Get(articleId);
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
		public async Task<ActionResult<ServiceResponse<List<Article>>>> AddArticle([FromBody]CreateArticleDto request)
		{
			var response = await articleService.AddArticle(request);
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
		public async Task<ActionResult<ServiceResponse<List<Article>>>> UpdateArticle([FromBody]CreateArticleDto request)
		{
			var response = await articleService.UpdateArticle(request);
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
		[HttpDelete("{articleId:guid}")]
		public async Task<ActionResult<ServiceResponse<List<Article>>>> DeleteArticle([FromRoute]Guid articleId)
		{
			var response = await articleService.DeleteArticle(articleId);
			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}
	}
}
