using AutoMapper;
using Microsoft.EntityFrameworkCore;
using testeLeadSoft.Data;
using testeLeadSoft.Dto.Article;
using testeLeadSoft.Dto.Category;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CategoryService
{
    public class CategoryService : ICategoryService
	{
		private readonly DataContext context;
		private readonly IMapper mapper;
		public CategoryService(DataContext context, IMapper mapper) 
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(CreateCategoryDto request)
		{
			var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

			try 
			{
				var newCategory = new Category()
				{
					Name = request.Name,
					Type = request.Type
				};

				context.Categories.Add(mapper.Map<Category>(newCategory));
				await context.SaveChangesAsync();

				serviceResponse.Data = await context.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToListAsync();
				serviceResponse.Message = "Category successfully created.";
			} catch(Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(Guid categoryId)
		{
			var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

			try
			{
				var dbCategory = await context.Categories.FindAsync(categoryId);
				if (dbCategory != null)
				{
					context.Categories.Remove(dbCategory);
					await context.SaveChangesAsync();

					serviceResponse.Data = await context.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToListAsync();
					serviceResponse.Message = "Category successfully deleted.";
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

		public async Task<ServiceResponse<List<GetCategoryDto>>> Get()
		{
			var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

			try
			{
				serviceResponse.Data = await context.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToListAsync();
				serviceResponse.Message = "All Categories successfully listed.";
			} catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(GetCategoryDto request)
		{
			var serviceResponse = new ServiceResponse<GetCategoryDto>();

			try
			{
				var dbCategory = await context.Categories.FindAsync(request.Id);
				if (dbCategory != null)
				{
					dbCategory.Name = request.Name;
					dbCategory.Type = request.Type;

					await context.SaveChangesAsync();

					serviceResponse.Data = mapper.Map<GetCategoryDto>(dbCategory);
					serviceResponse.Message = "Category successfully updated.";
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
