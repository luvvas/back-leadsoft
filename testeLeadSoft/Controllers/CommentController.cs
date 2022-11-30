using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto.Comment;
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
		public async Task<ActionResult<ServiceResponse<List<GetCommentDto>>>> Get()
		{
			var response = await commentService.Get();
			if (response.Data == null)
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
		public async Task<ActionResult<ServiceResponse<List<GetCommentDto>>>> AddComment([FromBody]CreateCommentDto request)
		{
			var response = await commentService.AddComment(request);
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
		public async Task<ActionResult<ServiceResponse<GetCommentDto>>> UpdateComment([FromBody]UpdateCommentDto request)
		{
			var response = await commentService.UpdateComment(request);
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
		[HttpDelete("{commentId:guid}")]
		public async Task<ActionResult<ServiceResponse<List<GetCommentDto>>>> DeleteComment([FromRoute]Guid commentId)
		{
			var response = await commentService.DeleteComment(commentId);
			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}
	}
}
