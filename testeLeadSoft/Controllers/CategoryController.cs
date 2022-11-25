using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;
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
		public async Task<ActionResult<List<Category>>> Get()
		{
			return Ok(await this.categoryService.Get());
		}

		/// <summary>
		/// Include a Category on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<List<Category>>> AddCategory(CreateCategoryDto request)
		{
			return Ok(await this.categoryService.AddCategory(request));
		}

		/// <summary>
		/// Update a category on Database
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionResult<List<Author>>> UpdateCategory(CreateCategoryDto request)
		{
			return Ok(await this.categoryService.UpdateCategory(request));
		}

		/// <summary>
		/// Delete a category on Database
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		[HttpDelete("{categoryId}")]
		public async Task<ActionResult<List<Category>>> DeleteCategory(Guid categoryId)
		{
			return Ok(await this.categoryService.DeleteCategory(categoryId));
		}

	}
}
