using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BloggyEFUppgift2
{
	internal class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public ICollection<Category> Categorys { get; set; }
		public ICollection<Tag> Tags { get; set; }

		public void DisplayPosts(BloggyDbContext context)
		{
			Console.WriteLine();
			var PostsList = context.Posts.ToList();
			foreach (var Post in PostsList)
			{
				Console.WriteLine("       " + Post.Id + ") " + Post.Title);
				Console.WriteLine("          " + Post.Text);
				Console.WriteLine("       -------------------");
				Console.WriteLine();
			}
		}

		public void CreateNewPost(BloggyDbContext context)
		{
			Console.Write("Ange det nya bloggpost titel:  ");
			var NewPostTitle = Console.ReadLine();
			Console.WriteLine();
			Console.WriteLine("Skriv in texten i den nya BLogposten: ");
			var NewPostText = Console.ReadLine();

			Post NewPost = new Post();
			NewPost.Title = NewPostTitle;
			NewPost.Text = NewPostText;

			// Kolla om det inte har lagts till tidigare
			var PostsList = context.Posts.ToList();
			bool PostExist = false;
			foreach (var item in PostsList)
			{
				if (item.Title == NewPost.Title)
				{
					if (item.Text == NewPost.Text) //Det kan vara samma titel men olika detaljer i stycket
					{
						Console.WriteLine();
						Console.WriteLine("     Det är inte ett nytt bloggpost !!.. Det finns redan där.");
						Console.WriteLine();
						PostExist = true;
					}
				}
			}
			if (PostExist == false)
			{
				context.Posts.Add(NewPost);
				context.SaveChanges();

				Console.WriteLine();
				Console.WriteLine("     >>> Den nya bloggposten har lagts till.");
				Console.WriteLine();
			}
		}

		public void DisplayPostsOfOneCategory(BloggyDbContext context)
		{
			Console.Write("Ange kategorinamnet: ");
			Category Cat = new Category();
			Cat.Name = Console.ReadLine();
			Console.WriteLine();

			var CatsList = context.Categories.ToList(); //Kontrollera om Kategori finns eller inte!
			bool CatExist = false;
			foreach (var item in CatsList)
			{
				if (item.Name == Cat.Name)
				{
					var PostsToDisplay = context.Entry(item)
							.Collection(b => b.Posts)
							.Query()
							.ToList();
					if (PostsToDisplay.Count > 0) //Finns det blogginlägg i denna kategori eller inte!
					{
						Console.WriteLine("   >>> alla blogginlägg är: ");
						Console.WriteLine();
						foreach (var post in PostsToDisplay)
						{
							Console.WriteLine("       " + post.Title);
							Console.WriteLine("       . . . . . . . .");
						}
					}
					else
					{
						Console.WriteLine("Kategorin " + Cat.Name + " är tom.");
					}
					Console.WriteLine();
					CatExist = true;
				}
			}
			if (CatExist == false)
			{
				Console.WriteLine();
				Console.WriteLine("     >>> Denna kategori finns inte.");
				Console.WriteLine();
			}
		}

		public void DisplayPostsOfOneTag(BloggyDbContext context)
		{
			Console.Write("Ange taggnamn: ");
			Tag tag = new Tag();
			tag.Name = Console.ReadLine();
			Console.WriteLine();

			var TagsList = context.Tags.ToList(); //Kontrollera om Tag finns eller inte!
			bool TagExist = false;
			foreach (var item in TagsList)
			{
				if (item.Name == tag.Name)
				{
					var PostsToDisplay = context.Entry(item)
							.Collection(b => b.Posts)
							.Query()
							.ToList();
					if (PostsToDisplay.Count > 0) //Finns det blogginlägg i denna tag eller inte!
					{
						Console.WriteLine("   >>> alla blogginlägg är: ");
						Console.WriteLine();
						foreach (var post in PostsToDisplay)
						{
							Console.WriteLine("       " + post.Title);
							Console.WriteLine("       . . . . . . . .");
						}
					}
					else
					{
						Console.WriteLine("Taggen " + tag.Name + " är tom.");
					}
					Console.WriteLine();
					TagExist = true;
				}
			}
			if (TagExist == false)
			{
				Console.WriteLine();
				Console.WriteLine("     >>> Den här taggen finns inte.");
				Console.WriteLine();
			}

		}
	
		public void AddPostToCategory(BloggyDbContext context)
		{
			Console.Write("Ange Blogpost namn: ");
			var PostToAdd = Console.ReadLine();
			Console.WriteLine();

			// Om Blogpost finns ta med PostId
			Post GetPost = new Post();
			GetPost = context.Posts.FirstOrDefault(x => x.Title == PostToAdd);
			
			if (GetPost == null)
			{
				Console.WriteLine(">>> Denna titel finns inte.");
				Console.WriteLine("    Gå till (4) För att lägga till det nya blogginlägget först.");
				Console.WriteLine("    ...........................................................");
				Console.WriteLine();
			}
			else
			{
				Console.Write("Ange kategorinamn: ");
				var CatName = Console.ReadLine();
				Console.WriteLine();

				// Är Kategori existerar
				Category GetCategory = new Category();
				GetCategory = context.Categories.FirstOrDefault(x => x.Name == CatName);

				if (GetCategory == null)
				{
					Console.WriteLine(">>> Den här kategorin finns inte.");
					Console.WriteLine("    Gå till (5) För att lägga till den nya kategorin först.");
					Console.WriteLine("	   .......................................................");
					Console.WriteLine();
				}
				else // Lägg till (Post+Category) men kolla först om de redan är kopplade till varandra
				{
					var CategoryPostList = context.Entry(GetCategory)
							.Collection(b => b.Posts)
							.Query()
							.ToList();
					bool Link = false;
					foreach (var item in CategoryPostList)
					{
						if (item.Title == PostToAdd)
						{
							Console.WriteLine("     >>> Det är redan kopplat ihop. ");
							Console.WriteLine("     ..............................");
							Link = true;
						}
					}
					if (Link == false)
					{
						GetCategory.Posts.Add(GetPost);
						context.Categories.Update(GetCategory);
						context.SaveChanges();
						Console.WriteLine(">>> Den nya länken ( " + CatName + GetPost +" ) har lagts till.");
						Console.WriteLine("....................................................");
						Console.WriteLine();
					}
				}
			}
		}

		public void AddPostToTag(BloggyDbContext context)
		{
			Console.Write("Ange Blogpost namn: ");
			var PostToAdd = Console.ReadLine();
			Console.WriteLine();

			// Om Blogpost finns ta med PostId
			Post GetPost = new Post();
			GetPost = context.Posts.FirstOrDefault(x => x.Title == PostToAdd);

			if (GetPost == null)
			{
				Console.WriteLine(">>> Denna titel finns inte.");
				Console.WriteLine("    Gå till (4) För att lägga till det nya blogginlägget först.");
				Console.WriteLine("    ...........................................................");
				Console.WriteLine();
			}
			else
			{
				Console.Write("Ange Taggnamn: ");
				var TagName = Console.ReadLine();
				Console.WriteLine();

				// Är Tag existerar
				Tag GetTag = new Tag();
				GetTag = context.Tags.FirstOrDefault(x => x.Name == TagName);

				if (GetTag == null)
				{
					Console.WriteLine(">>> Den här taggen finns inte.");
					Console.WriteLine("    Gå till (6) För att lägga till den nya taggen först.");
					Console.WriteLine("	   .......................................................");
					Console.WriteLine();
				}
				else // Lägg till (Post+Tag) men kolla först om de redan är kopplade till varandra
				{
					var TagPostList = context.Entry(GetTag)
							.Collection(b => b.Posts)
							.Query()
							.ToList();
					bool Link = false;
					foreach (var item in TagPostList)
					{
						if (item.Title == PostToAdd)
						{
							Console.WriteLine("     >>> Det är redan kopplat ihop. ");
							Console.WriteLine("     ..............................");
							Link = true;
						}
					}
					if (Link == false)
					{
						GetTag.Posts.Add(GetPost);
						context.Tags.Update(GetTag);
						context.SaveChanges();
						Console.WriteLine(">>> " + GetPost + " har tagts till " + TagName  );
						Console.WriteLine("....................................................");
						Console.WriteLine();
					}
				}
			}
		}
	}
}


