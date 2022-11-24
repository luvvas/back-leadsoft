using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly DataContext context;

		public CategoryController(DataContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Category>>> Get()
		{
			return Ok(await this.context.Categories.ToListAsync());
		}

		[HttpPost]
		public async Task<ActionResult<List<Category>>> AddCategory(CreateCategoryDto request)
		{
			var newCategory = new Category()
			{
				Name = request.Name,
				Type = request.Type
			};

			this.context.Categories.Add(newCategory);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Categories.ToListAsync());
		}

		[HttpPut]
		public async Task<ActionResult<List<Author>>> UpdateCategory(CreateCategoryDto request)
		{
			var dbCategory = await this.context.Categories.FindAsync(request.Id);
			if (dbCategory == null)
			{
				return BadRequest("Category not found.");
			}

			dbCategory.Name = request.Name;
			dbCategory.Type = request.Type;

			await this.context.SaveChangesAsync();

			return Ok(await this.context.Authors.ToListAsync());
		}

		[HttpDelete("categoryId")]
		public async Task<ActionResult<List<Category>>> DeleteCategory(Guid categoryId)
		{
			var dbCategory = await this.context.Categories.FindAsync(categoryId);
			if (dbCategory == null)
			{
				return BadRequest("Category not found");
			}

			//Recebe o nome
			//Console.WriteLine(dbCategory.Name);

			this.context.Categories.Remove(dbCategory);
			await this.context.SaveChangesAsync();

			return Ok(await this.context.Categories.ToListAsync());
		}

	}
}
