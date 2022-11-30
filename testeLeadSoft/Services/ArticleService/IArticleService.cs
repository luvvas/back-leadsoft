using testeLeadSoft.Dto.Article;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.ArticleService
{
    public interface IArticleService
	{
		Task<ServiceResponse<List<GetArticleDto>>> Get();
		Task<ServiceResponse<GetArticleDto>> Get(Guid authorId);
		Task<ServiceResponse<List<GetArticleDto>>> AddArticle(CreateArticleDto request);
		Task<ServiceResponse<GetArticleDto>> UpdateArticle(UpdateArticleDto request);
		Task<ServiceResponse<List<GetArticleDto>>> DeleteArticle(Guid articleId);
	}
}
