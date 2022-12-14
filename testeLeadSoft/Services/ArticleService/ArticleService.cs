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
				var article = await context.Articles
					.Where(a => a.Title == request.Title)
					.FirstOrDefaultAsync();

				if (author != null && article == null)
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
				} else if (author == null)
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Author not found.";
				} else if (article != null)
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "This Article already Exists.";
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
				var dbArticle = await context.Articles.Include(a => a.Comments).ToListAsync();
				serviceResponse.Data = dbArticle.Select(a => mapper.Map<GetArticleDto>(a)).ToList();
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
				var article = await context.Articles.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == articleId);
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
				var dbArticleAlreadyExists = await context.Articles
					.Where(a => a.Title == request.Title)
					.FirstOrDefaultAsync();

				if(dbArticle != null && dbArticleAlreadyExists == null)
				{
					dbArticle.Title = request.Title;
					dbArticle.Description = request.Description;
					dbArticle.Text = request.Text;

					await context.SaveChangesAsync();

					serviceResponse.Data = mapper.Map<GetArticleDto>(dbArticle);
					serviceResponse.Message = "Article successfully updated.";
				} else if (dbArticle == null)
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Article not found.";
				} else if (dbArticleAlreadyExists != null)
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "This Article already exists.";
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
