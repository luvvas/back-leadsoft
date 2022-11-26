using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.ArticleService
{
	public class ArticleService : IArticleService
	{
		private readonly DataContext context;
		public ArticleService(DataContext context) 
		{
			this.context = context;
		}

		public async Task<ServiceResponse<List<Article>>> AddArticle(CreateArticleDto request)
		{
			var serviceResponse = new ServiceResponse<List<Article>>();

			try
			{
				var author = await this.context.Authors.FindAsync(request.AuthorId);

				if (author != null)
				{
					var category = await this.context.Categories.FindAsync(request.CategoryId);
					if (category != null)
					{
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

						serviceResponse.Data = await this.context.Articles.ToListAsync();
					} else
					{
						serviceResponse.Success = false;
						serviceResponse.Message = "Category not found.";
					}
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Author not found.";
				}
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Article>>> DeleteArticle(Guid articleId)
		{
			var serviceResponse = new ServiceResponse<List<Article>>();

			try
			{
				var dbArticle = await this.context.Articles.FindAsync(articleId);
				if (dbArticle != null)
				{
					this.context.Articles.Remove(dbArticle);
					await this.context.SaveChangesAsync();

					serviceResponse.Data = await this.context.Articles.ToListAsync();
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Article not found.";
				}
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Article>>> Get(Guid authorId)
		{
			var serviceResponse = new ServiceResponse<List<Article>>();

			try
			{
				var articles = await this.context.Articles
				.Where(a => a.AuthorId == authorId)
				.ToListAsync();

				if (articles != null)
				{
					serviceResponse.Data = article;
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Article not found.";
				}
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Article>>> UpdateArticle(CreateArticleDto request)
		{
			var serviceResponse = new ServiceResponse<List<Article>>();

			try
			{
				var dbAuthor = await this.context.Authors.FindAsync(request.AuthorId);
				if(dbAuthor != null)
				{
					if(dbArticle != null)
					{
						dbArticle.Title = request.Title;
						dbArticle.Description = request.Description;
						dbArticle.Text = request.Text;

						await this.context.SaveChangesAsync();

						serviceResponse.Data = await this.context.Articles.ToListAsync();
					} else
					{
						serviceResponse.Success = false;
						serviceResponse.Message = "Article not found";
					}
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Author not found";
				}
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}
	}
}
