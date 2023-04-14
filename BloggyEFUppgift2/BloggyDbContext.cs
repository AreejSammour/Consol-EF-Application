using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BloggyEFUppgift2
{
	internal class BloggyDbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }

		string ConnectionString = "Server= ;Database=Bloggy;Trusted_Connection=True;Encrypt=False";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConnectionString);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Post post = new Post() { Id = 1, Title = "Post1", Text = "Text1" };
			Post post1 = new Post() { Id = 1, Title = "Volvo EX90", Text = "Sju säten, ett stort bagageutrymme och redo för alla äventyr" };
			Post post2 = new Post() { Id = 2, Title = "XC90 Recharge", Text = "En ny definition av lyx. Fokus på detaljer och en vilja att maximera din komfort gör varje mil i XC90 Recharge till ett nöje." };
			Post post3 = new Post() { Id = 3, Title = "S60 Recharge", Text = "Detaljerna spelar roll. Interiören har vi skapat för att beröra alla dina sinnen." };
			Post post4 = new Post() { Id = 4, Title = "V90 Cross Country", Text = "Äventyrsbilen. Den lyxiga kombin med Google inbyggt, designad för att utforska." };

			//Category category = new Category() { Id = 1, Name = "Cat1" };
			Category category1 = new Category() { Id = 1, Name = "SUV" };
			Category category2 = new Category() { Id = 2, Name = "SEDAN" };
			Category category3 = new Category() { Id = 3, Name = "KOMBI" };

			//Tag tag = new Tag() { Id = 1, Name = "Tag1" };
			Tag tag1 = new Tag() { Id = 1, Name = "Elbilar" };
			Tag tag2 = new Tag() { Id = 2, Name = "Hybridbilar" };
			Tag tag3 = new Tag() { Id = 3, Name = "Övriga bilar" };

			modelBuilder.Entity<Post>().HasData(post1,post2, post3, post4);
			modelBuilder.Entity<Category>().HasData(category1, category2, category3);
			modelBuilder.Entity<Tag>().HasData(tag1, tag2, tag3);



			modelBuilder.Entity("CategoryPost").HasData(
				new { CategorysId = 1, PostsId = 1 });
			

			modelBuilder.Entity("PostTag").HasData(
				new { PostsId = 1, TagsId = 1 });

		}
	}
}
