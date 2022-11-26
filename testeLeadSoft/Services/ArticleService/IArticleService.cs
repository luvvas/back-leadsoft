using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.ArticleService
{
	public interface IArticleService
	{
		Task<ServiceResponse<List<Article>>> Get(Guid authorId);
		Task<ServiceResponse<List<Article>>> AddArticle(CreateArticleDto request);
		Task<ServiceResponse<List<Article>>> UpdateArticle(CreateArticleDto request);
		Task<ServiceResponse<List<Article>>> DeleteArticle(Guid articleId);
	}
}
