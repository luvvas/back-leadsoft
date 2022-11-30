using AutoMapper;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto.Article;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.ArticleService
{
    public class ArticleService : IArticleService
	{
		private readonly DataContext context;
		private readonly IMapper mapper;
		public ArticleService(DataContext context, IMapper mapper) 
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetArticleDto>>> AddArticle(CreateArticleDto request)
		{
			var serviceResponse = new ServiceResponse<List<GetArticleDto>>();

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

						context.Articles.Add(mapper.Map<Article>(newArticle));
						await context.SaveChangesAsync();

						serviceResponse.Data = await context.Articles.Select(a => mapper.Map<GetArticleDto>(a)).ToListAsync();
						serviceResponse.Message = "Article successfully created.";
					}
					else
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

		public async Task<ServiceResponse<List<GetArticleDto>>> DeleteArticle(Guid articleId)
		{
			var serviceResponse = new ServiceResponse<List<GetArticleDto>>();

			try
			{
				var dbArticle = await context.Articles.FindAsync(articleId);
				if (dbArticle != null)
				{
					context.Articles.Remove(dbArticle);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Articles.Select(a => mapper.Map<GetArticleDto>(a)).ToListAsync();
					serviceResponse.Message = "Article successfully deleted.";
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

		public async Task<ServiceResponse<List<GetArticleDto>>> Get() 
		{ 
			var serviceResponse = new ServiceResponse<List<GetArticleDto>>();

			try
			{
				serviceResponse.Data = await context.Articles.Select(a => mapper.Map<GetArticleDto>(a)).ToListAsync();
				serviceResponse.Message = "All Articles successfully listed.";
			} catch(Exception ex) 
			{ 
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message; 
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetArticleDto>> Get(Guid articleId)
		{
			var serviceResponse = new ServiceResponse<GetArticleDto>();

			try
			{
				var article = await context.Articles.FindAsync(articleId);
				if (article != null)
				{
					serviceResponse.Data = mapper.Map<GetArticleDto>(article);
					serviceResponse.Message = "Article successfully listed.";
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

		public async Task<ServiceResponse<GetArticleDto>> UpdateArticle(UpdateArticleDto request)
		{
			var serviceResponse = new ServiceResponse<GetArticleDto>();

			try
			{
				var dbArticle = await context.Articles.FindAsync(request.Id);
				if(dbArticle != null)
				{
					dbArticle.Title = request.Title;
					dbArticle.Description = request.Description;
					dbArticle.Text = request.Text;

					await context.SaveChangesAsync();

					serviceResponse.Data = mapper.Map<GetArticleDto>(dbArticle);
					serviceResponse.Message = "Article successfully updated.";
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
