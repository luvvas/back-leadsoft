using Microsoft.AspNetCore.Mvc;

using testeLeadSoft.Dto;
using testeLeadSoft.Models;
using testeLeadSoft.Services.CommentService;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService commentService;

		public CommentController(ICommentService commentService) 
		{
			this.commentService = commentService;
		}

		/// <summary>
		/// Get all comments on Database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Comment>>>> Get()
		{
			var response = await this.commentService.Get();
			if(response.Data == null)
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Include a comment on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<Comment>>>> AddComment(CreateCommentDto request)
		{
			var response = await this.commentService.AddComment(request);
			if (response.Data == null)
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a comment on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<ServiceResponse<List<Comment>>>> UpdateComment(CreateCommentDto request)
		{
			var response = await this.commentService.UpdateComment(request);
			if (response.Data == null)
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a comment on Database
		/// </summary>
		/// <param name="commentId"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<ActionResult<ServiceResponse<List<Comment>>>> DeleteComment(Guid commentId)
		{
			var response = await this.commentService.DeleteComment(commentId);
			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}
	}
}
