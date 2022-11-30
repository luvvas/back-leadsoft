using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto.Comment;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CommentService
{
    public interface ICommentService
	{
		Task<ServiceResponse<List<GetCommentDto>>> Get();
		Task<ServiceResponse<List<GetCommentDto>>> AddComment(CreateCommentDto request);
		Task<ServiceResponse<GetCommentDto>> UpdateComment(UpdateCommentDto request);
		Task<ServiceResponse<List<GetCommentDto>>> DeleteComment(Guid commentId);
	}
}
