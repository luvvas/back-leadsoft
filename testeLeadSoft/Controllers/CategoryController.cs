using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;
using testeLeadSoft.Services.AuthorService;
using testeLeadSoft.Services.CategoryService;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		/// <summary>
		/// Get all categories on Database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Category>>>> Get()
		{
			var response = await categoryService.Get();
			if(response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Include a Category on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<Category>>>> AddCategory(CreateCategoryDto request)
		{
			var response = await this.categoryService.AddCategory(request);
			if(response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Update a category on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> UpdateCategory(CreateCategoryDto request)
		{
			var response = await this.categoryService.UpdateCategory(request);
			if(response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		/// <summary>
		/// Delete a category on Database
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		[HttpDelete("{categoryId}")]
		public async Task<ActionResult<ServiceResponse<List<Category>>>> DeleteCategory(Guid categoryId)
		{
			var response = await this.categoryService.DeleteCategory(categoryId);
			if(response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

	}
}
