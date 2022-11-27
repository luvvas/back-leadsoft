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
				var author = await context.Authors.FindAsync(request.AuthorId);

				if (author != null)
				{
					var category = await context.Categories.FindAsync(request.CategoryId);
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

						context.Articles.Add(newArticle);
						await context.SaveChangesAsync();

						serviceResponse.Data = await context.Articles.ToListAsync();
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
				var dbArticle = await context.Articles.FindAsync(articleId);
				if (dbArticle != null)
				{
					context.Articles.Remove(dbArticle);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Articles.ToListAsync();
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

		public async Task<ServiceResponse<List<Article>>> Get() { 
			var serviceResponse = new ServiceResponse<List<Article>>();

			try
			{
				serviceResponse.Data = await context.Articles.ToListAsync();
			} catch(Exception ex) 
			{ 
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message; 
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<Article>> Get(Guid articleId)
		{
			var serviceResponse = new ServiceResponse<Article>();

			try
			{
				var article = await context.Articles.FindAsync(articleId);

				if (article != null)
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
				var dbArticle = await context.Articles.FindAsync(request.Id);
				if(dbArticle != null)
				{
					dbArticle.Title = request.Title;
					dbArticle.Description = request.Description;
					dbArticle.Text = request.Text;

					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Articles.ToListAsync();
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Article not found";
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
