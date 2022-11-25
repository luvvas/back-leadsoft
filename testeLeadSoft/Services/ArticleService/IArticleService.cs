using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.ArticleService
{
	public interface IArticleService
	{
		Task<List<Article>> Get(Guid authorId);
		Task<List<Article>> AddArticle(CreateArticleDto request);
		Task<List<Article>> UpdateArticle(CreateArticleDto request);
		Task<List<Article>> DeleteArticle(Guid articleId);
	}
}
