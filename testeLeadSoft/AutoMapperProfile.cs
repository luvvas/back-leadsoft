using AutoMapper;
using testeLeadSoft.Dto.Article;
using testeLeadSoft.Dto.Author;
using testeLeadSoft.Dto.Category;
using testeLeadSoft.Dto.Comment;
using testeLeadSoft.Models;

namespace testeLeadSoft
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<Author, GetAuthorDto>();
			CreateMap<CreateAuthorDto, Author>();
			CreateMap<Article, GetArticleDto>();
			CreateMap<CreateArticleDto, Article>();
			CreateMap<Category, GetCategoryDto>();
			CreateMap<CreateCategoryDto, Category>();
			CreateMap<Comment, GetCommentDto>();
			CreateMap<CreateCommentDto, Comment>();
		}
	}
}
