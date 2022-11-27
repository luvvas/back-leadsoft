using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CommentService
{
	public interface ICommentService
	{
		Task<ServiceResponse<List<Comment>>> Get();
		Task<ServiceResponse<List<Comment>>> AddComment(CreateCommentDto request);
		Task<ServiceResponse<List<Comment>>> UpdateComment(CreateCommentDto request);
		Task<ServiceResponse<List<Comment>>> DeleteComment(Guid commentId);
	}
}
