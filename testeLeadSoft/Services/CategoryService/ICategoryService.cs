using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CategoryService
{
	public interface ICategoryService
	{
		Task<ServiceResponse<List<Category>>> Get();
		Task<ServiceResponse<List<Category>>> AddCategory(CreateCategoryDto request);
		Task<ServiceResponse<List<Category>>> UpdateCategory(CreateCategoryDto request);
		Task<ServiceResponse<List<Category>>> DeleteCategory(Guid categoryId);
	}
}
