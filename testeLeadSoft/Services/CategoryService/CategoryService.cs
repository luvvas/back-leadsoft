using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly DataContext context;
		public CategoryService(DataContext context) 
		{
			this.context = context;
		}

		public async Task<List<Category>> AddCategory(CreateCategoryDto request)
		{
			var newCategory = new Category()
			{
				Name = request.Name,
				Type = request.Type
			};

			this.context.Categories.Add(newCategory);
			await this.context.SaveChangesAsync();

			return await this.context.Categories.ToListAsync();
		}

		public async Task<List<Category>> DeleteCategory(Guid categoryId)
		{
			var dbCategory = await this.context.Categories.FindAsync(categoryId);
			if (dbCategory == null)
			{
				// BadRequest("Category not found")
				return null;
			}

			//Recebe o nome
			//Console.WriteLine(dbCategory.Name);

			this.context.Categories.Remove(dbCategory);
			await this.context.SaveChangesAsync();

			return await this.context.Categories.ToListAsync();
		}

		public async Task<List<Category>> Get()
		{
			return await this.context.Categories.ToListAsync();
		}

		public async Task<List<Category>> UpdateCategory(CreateCategoryDto request)
		{
			var dbCategory = await this.context.Categories.FindAsync(request.Id);
			if (dbCategory == null)
			{
				// BadRequest("Category not found.")
				return null;
			}

			dbCategory.Name = request.Name;
			dbCategory.Type = request.Type;

			await this.context.SaveChangesAsync();

			return await this.context.Categories.ToListAsync();
		}
	}
}
