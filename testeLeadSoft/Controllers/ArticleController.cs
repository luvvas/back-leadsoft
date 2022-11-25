using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticleController : ControllerBase
	{
		private readonly DataContext context;
		public ArticleController(DataContext context) 
		{
			this.context = context;
		}

		/// <summary>
		/// Get a article by it's id
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpGet("{authorId}")]
		public async Task<ActionResult<List<Article>>> Get(Guid authorId)
		{
			var articles = await this.context.Articles
				.Where(a => a.AuthorId == authorId)
				.ToListAsync();

			return articles;
		}

		/// <summary>
		/// Include a article on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<List<Article>>> AddArticle(CreateArticleDto request)
		{
			var author = await this.context.Authors.FindAsync(request.AuthorId);
			if(author == null)
			{
				return BadRequest("Author not found.");
			}

			var category = await this.context.Categories.FindAsync(request.CategoryId);
			if(category == null)
			{
				return BadRequest("Category not found.");
			}

			var newArticle = new Article
			{
				Title = request.Title,
				Description = request.Description,
				Text = request.Text,
				Author = author,
				Category = category
			};

			this.context.Articles.Add(newArticle);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Articles.ToListAsync());
		}

		/// <summary>
		/// Update a article on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<List<Article>>> UpdateArticle(CreateArticleDto request)
		{
			var dbAuthor = await this.context.Authors.FindAsync(request.AuthorId);
			if(dbAuthor == null)
			{
				return BadRequest("Author not found.");
			}

			var dbArticle = await this.context.Articles.FindAsync(request.Id); 
			if(dbArticle == null)
			{
				return BadRequest("Article not found.");
			}

			dbArticle.Title = request.Title;
			dbArticle.Description = request.Description;
			dbArticle.Text = request.Text;

			await this.context.SaveChangesAsync();

			return Ok(await this.context.Articles.ToListAsync());
		}

		/// <summary>
		/// Delete a article on Database
		/// </summary>
		/// <param name="articleId"></param>
		/// <returns></returns>
		[HttpDelete("{articleId}")]
		public async Task<ActionResult<List<Article>>> DeleteArticle(Guid articleId)
		{
			var dbArticle = await this.context.Articles.FindAsync(articleId);
			if (dbArticle == null)
			{
				return BadRequest("Article not found.");
			}

			this.context.Articles.Remove(dbArticle);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Articles.ToListAsync());
		}
	}
}
