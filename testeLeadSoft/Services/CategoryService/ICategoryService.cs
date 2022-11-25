using Microsoft.AspNetCore.Mvc;
using testeLeadSoft.Dto;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CategoryService
{
	public interface ICategoryService
	{
		Task<List<Category>> Get();
		Task<List<Category>> AddCategory(CreateCategoryDto request);
		Task<List<Category>> UpdateCategory(CreateCategoryDto request);
		Task<List<Category>> DeleteCategory(Guid categoryId);
	}
}
