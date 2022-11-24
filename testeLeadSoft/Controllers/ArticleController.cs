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

		[HttpGet("{authorId}")]
		public async Task<ActionResult<List<Article>>> Get(Guid authorId)
		{
			var articles = await this.context.Articles
				.Where(a => a.AuthorId == authorId)
				.ToListAsync();

			return articles;
		}

		[HttpPost]
		public async Task<ActionResult<List<Article>>> AddArticle(CreateArticleDto request)
		{
			var author = await this.context.Authors.FindAsync(request.AuthorId);
			if(author == null)
			{
				return BadRequest("Author not found.");
			}

			// Pode ser que o usuário passe o categoryId ou não
			// Resolver isso
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

		[HttpDelete("{ArticleId}")]
		public async Task<ActionResult<List<Article>>> DeleteArticle(Guid ArticleId)
		{
			var dbArticle = await this.context.Articles.FindAsync(ArticleId);
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
