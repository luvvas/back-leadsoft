using AutoMapper;
using testeLeadSoft.Dto.Author;
using testeLeadSoft.Models;

namespace testeLeadSoft
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<Author, GetAuthorDto>();
			CreateMap<CreateAuthorDto, Author>();
		}
	}
}
