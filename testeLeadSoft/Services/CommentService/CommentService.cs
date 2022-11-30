using AutoMapper;
using Microsoft.EntityFrameworkCore;

using testeLeadSoft.Data;
using testeLeadSoft.Dto.Comment;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CommentService
{
    public class CommentService : ICommentService
	{
		private readonly DataContext context;
		private readonly IMapper mapper;

		public CommentService(DataContext context, IMapper mapper) 
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetCommentDto>>> Get()
		{
			var serviceResponse = new ServiceResponse<List<GetCommentDto>>();

			try
			{
				serviceResponse.Data = await context.Comments.Select(c => mapper.Map<GetCommentDto>(c)).ToListAsync();
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCommentDto>>> AddComment(CreateCommentDto request)
		{
			var serviceResponse = new ServiceResponse<List<GetCommentDto>>();

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

					serviceResponse.Data = await context.Comments.Select(c => mapper.Map<GetCommentDto>(c)).ToListAsync();
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

		public async Task<ServiceResponse<GetCommentDto>> UpdateComment(UpdateCommentDto request)
		{
			var serviceResponse = new ServiceResponse<GetCommentDto>();

			try 
			{
				var dbComment = await context.Comments.FindAsync(request.Id);
				if(dbComment != null)
				{
					dbComment.Text = request.Text;

					await context.SaveChangesAsync();

					serviceResponse.Data = mapper.Map<GetCommentDto>(dbComment);
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

		public async Task<ServiceResponse<List<GetCommentDto>>> DeleteComment(Guid commentId)
		{
			var serviceResponse = new ServiceResponse<List<GetCommentDto>>();

			try
			{
				var dbComment = await context.Comments.FindAsync(commentId);
				if (dbComment != null)
				{
					context.Comments.Remove(dbComment);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Comments.Select(c => mapper.Map<GetCommentDto>(c)).ToListAsync();
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
