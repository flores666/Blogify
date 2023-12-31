﻿using lib.Blog.Objects;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.DataAccess
{
	[Obsolete]
	public class BlogContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

		public BlogContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(Data.CONN_STRING_MAIN)
				.UseSnakeCaseNamingConvention();
		}
	}
}