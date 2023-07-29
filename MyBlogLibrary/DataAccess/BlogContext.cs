using Microsoft.EntityFrameworkCore;
using MyBlogLibrary.Objects;

namespace MyBlogLibrary.DataAccess
{
	public class BlogContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

		public BlogContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=YouBlog;Username=postgres;Password=str8shotx");
		}
	}
}
