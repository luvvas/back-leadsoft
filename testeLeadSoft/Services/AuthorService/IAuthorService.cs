using testeLeadSoft.Dto.Author;
using testeLeadSoft.Models;

namespace testeLeadSoft.Services.AuthorService
{
    public interface IAuthorService
		{
			Task<ServiceResponse<List<GetAuthorDto>>> Get();
			Task<ServiceResponse<List<GetAuthorDto>>> AddAuthor(CreateAuthorDto request);
			Task<ServiceResponse<GetAuthorDto>> Get(Guid authorId);
			Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(GetAuthorDto request);
			Task<ServiceResponse<List<GetAuthorDto>>> DeleteAuthor(Guid authorId);
		}
}