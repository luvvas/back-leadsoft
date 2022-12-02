using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using AutoMapper;
using Xunit;
using Moq;

using testeLeadSoft.Data;
using testeLeadSoft.Models;
using testeLeadSoft.Services.AuthorService;
using FluentAssertions;
using testeLeadSoft.Dto.Author;

namespace testeLeadSoft.Tests.Services
{
	public class AuthorServiceTests
	{
		private async Task<DataContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			var databaseContext = new DataContext(options);
			databaseContext.Database.EnsureCreated();

			if(await databaseContext.Authors.CountAsync() <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					databaseContext.Authors.Add(
						new Author()
						{
							FirstName = "Lucas",
							LastName = "Machado",
							Age = 22,
						}
					);
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}

		private readonly IMapper mapper;
		public AuthorServiceTests() 
		{
			this.mapper = new Mock<IMapper>().Object;
		}

		[Fact]
		public async void AuthorService_GetAuthors_ReturnAuthors()
		{
			// Arrange
			var dbContext = await GetDatabaseContext();
			var authorService = new AuthorService(dbContext, mapper);

			// Act
			var result = authorService.Get();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Task<ServiceResponse<List<GetAuthorDto>>>>();
		}
	}
}
