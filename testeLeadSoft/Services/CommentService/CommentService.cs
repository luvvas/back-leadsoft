using Microsoft.EntityFrameworkCore;

using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CommentService
{
	public class CommentService : ICommentService
	{
		private readonly DataContext context;

		public CommentService(DataContext context) 
		{
			this.context = context;
		}

		public async Task<ServiceResponse<List<Comment>>> Get()
		{
			var serviceResponse = new ServiceResponse<List<Comment>>();

			try
			{
				serviceResponse.Data = await context.Comments.ToListAsync();
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Comment>>> AddComment(CreateCommentDto request)
		{
			var serviceResponse = new ServiceResponse<List<Comment>>();

			try
			{
				var article = await context.Articles.FindAsync(request.ArticleId);

				if(article != null) 
				{
					var newComment = new Comment
					{
						Text = request.Text,
						Article = article
					};

					context.Comments.Add(newComment);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Comments.ToListAsync();
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

		public async Task<ServiceResponse<List<Comment>>> UpdateComment(CreateCommentDto request)
		{
			var serviceResponse = new ServiceResponse<List<Comment>>();

			try 
			{
				var dbComment = await context.Comments.FindAsync(request.Id);
				if(dbComment != null)
				{
					dbComment.Text = request.Text;

					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Comments.ToListAsync();
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Comment not found.";
				}
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Comment>>> DeleteComment(Guid commentId)
		{
			var serviceResponse = new ServiceResponse<List<Comment>>();

			try
			{
				var dbComment = await context.Comments.FindAsync(commentId);
				if (dbComment != null)
				{
					context.Comments.Remove(dbComment);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Comments.ToListAsync();
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Comment not found.";
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
