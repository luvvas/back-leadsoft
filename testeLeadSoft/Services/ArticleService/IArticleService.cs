using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.ArticleService
{
	public interface IArticleService
	{
		Task<ActionResult<List<Article>>> Get(Guid authorId);
		Task<ActionResult<List<Article>>> AddArticle(CreateArticleDto request);
		Task<ActionResult<List<Article>>> UpdateArticle(CreateArticleDto request);
		Task<ActionResult<List<Article>>> DeleteArticle(Guid articleId);
	}
}
