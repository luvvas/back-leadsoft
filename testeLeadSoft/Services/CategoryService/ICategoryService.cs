using testeLeadSoft.Dto.Article;
using testeLeadSoft.Dto.Category;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.CategoryService
{
    public interface ICategoryService
	{
		Task<ServiceResponse<List<GetCategoryDto>>> Get();
		Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(CreateCategoryDto request);
		Task<ServiceResponse<GetCategoryDto>> UpdateCategory(GetCategoryDto request);
		Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(Guid categoryId);
	}
}
