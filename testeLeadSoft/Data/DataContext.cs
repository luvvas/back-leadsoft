using Microsoft.EntityFrameworkCore;

using testeLeadSoft.Models;

namespace testeLeadSoft.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base (options) { }
	
		public DbSet<Author> Authors { get; set; }
		public DbSet<Article> Articles { get; set; }
	}
}
