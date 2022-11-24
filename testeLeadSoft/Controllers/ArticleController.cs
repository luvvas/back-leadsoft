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

		[HttpGet]
		public async Task<ActionResult<List<Article>>> Get(Guid authorId)
		{
			var articles = await this.context.Articles
				.Where(a => a.AuthorId == authorId)
				.ToListAsync();

			return articles;
		}

		[HttpPost]
		public async Task<ActionResult<List<Author>>> AddArticle(CreateArticleDto request)
		{
			var author = await this.context.Authors.FindAsync(request.AuthorId);
			if(author == null)
			{
				return BadRequest("Author not found");
			}

			var newArticle = new Article
			{
				Title = request.Title,
				Description = request.Description,
				Text = request.Text,
				Author = author
			};

			this.context.Articles.Add(newArticle);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Articles.ToListAsync());
		}
	}
}
