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

		public async Task<ServiceResponse<List<Category>>> AddCategory(CreateCategoryDto request)
		{
			var serviceResponse = new ServiceResponse<List<Category>>();

			try 
			{
				var newCategory = new Category()
				{
					Name = request.Name,
					Type = request.Type
				};

				context.Categories.Add(newCategory);
				await context.SaveChangesAsync();

				serviceResponse.Data = await context.Categories.ToListAsync();
			} catch(Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Category>>> DeleteCategory(Guid categoryId)
		{
			var serviceResponse = new ServiceResponse<List<Category>>();

			try
			{
				var dbCategory = await context.Categories.FindAsync(categoryId);
				if (dbCategory != null)
				{
					context.Categories.Remove(dbCategory);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Categories.ToListAsync();
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Category not found.";
				}
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Category>>> Get()
		{
			var serviceResponse = new ServiceResponse<List<Category>>();

			try
			{
				serviceResponse.Data = await context.Categories.ToListAsync();
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Category>>> UpdateCategory(CreateCategoryDto request)
		{
			var serviceResponse = new ServiceResponse<List<Category>>();

			try
			{
				var dbCategory = await context.Categories.FindAsync(request.Id);
				if (dbCategory != null)
				{
					dbCategory.Name = request.Name;
					dbCategory.Type = request.Type;

					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Categories.ToListAsync();
				} else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Category not found.";
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
