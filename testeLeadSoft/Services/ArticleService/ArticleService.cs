﻿using Microsoft.AspNetCore.Mvc;
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

		public async Task<ActionResult<List<Article>>> AddArticle(CreateArticleDto request)
		{
			var author = await this.context.Authors.FindAsync(request.AuthorId);
			if (author == null)
			{
				// BadRequest("Author not found.")
				return null;
			}

			var category = await this.context.Categories.FindAsync(request.CategoryId);
			if (category == null)
			{
				// BadRequest("Category not found.")
				return null;
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

			return await this.context.Articles.ToListAsync();
		}

		public async Task<ActionResult<List<Article>>> DeleteArticle(Guid articleId)
		{
			var dbArticle = await this.context.Articles.FindAsync(articleId);
			if (dbArticle == null)
			{
				// BadRequest("Article not found.")
				return null;
			}

			this.context.Articles.Remove(dbArticle);
			await this.context.SaveChangesAsync();

			return await this.context.Articles.ToListAsync();
		}

		public async Task<ActionResult<List<Article>>> Get(Guid authorId)
		{
			var articles = await this.context.Articles
				.Where(a => a.AuthorId == authorId)
				.ToListAsync();

			return articles;
		}

		public async Task<ActionResult<List<Article>>> UpdateArticle(CreateArticleDto request)
		{
			var dbAuthor = await this.context.Authors.FindAsync(request.AuthorId);
			if (dbAuthor == null)
			{
				// BadRequest("Author not found.")
				return null;
			}

			var dbArticle = await this.context.Articles.FindAsync(request.Id);
			if (dbArticle == null)
			{
				// BadRequest("Article not found.")
				return null;
			}

			dbArticle.Title = request.Title;
			dbArticle.Description = request.Description;
			dbArticle.Text = request.Text;

			await this.context.SaveChangesAsync();

			return await this.context.Articles.ToListAsync();
		}
	}
}
