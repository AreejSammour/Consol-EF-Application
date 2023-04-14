using BloggyEFUppgift2;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;

internal class Program
{
	private static void Main(string[] args)
	{
		BloggyDbContext Context = new BloggyDbContext();

		Console.WriteLine("Välkommen till Volvo blogg-app");
		Console.WriteLine("==============================");
		Console.WriteLine();

		bool Loop = true;

		while (Loop)
		{
			Console.WriteLine("Välj ett av följande alternativ: ");
			Console.WriteLine();
			Console.WriteLine("   1. Visa alla poster.");
			Console.WriteLine("   2. Visa namnen på alla kategorier.");
			Console.WriteLine("   3. Visa alla taggars namn.");
			Console.WriteLine("   4. Lägg till en ny bloggpost.");
			Console.WriteLine("   5. Lägg till en ny kategori.");
			Console.WriteLine("   6. Lägg till nytt taggnamn.");
			Console.WriteLine("   7. Visa alla blogginlägg från en specifik kategori.");
			Console.WriteLine("   8. Visa alla blogginlägg taggade för en specifik tagg.");
			Console.WriteLine("   9. Lägg till ett existerande bloggpost i en befintlig kategori.");
			Console.WriteLine("   10. Tagga en bloggpost.");
			Console.WriteLine();
			Console.WriteLine("För att avsluta skriv in (q/Q).");
			Console.WriteLine();
			Console.Write("Ange ditt val: ");
			var UserChoice = Console.ReadLine();
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Green;
			if (UserChoice == "1")
			{
				Console.WriteLine("     Blogginlägg är följande: ");
				Post PostsToDisplay = new Post();
				PostsToDisplay.DisplayPosts(Context);
			}
			else if (UserChoice == "2")
			{
				Console.WriteLine("     Alla kategoriers namn är: ");
				Category CategorysToDisplay = new Category();
				CategorysToDisplay.DisplayCategories(Context);
			}
			else if (UserChoice == "3")
			{
				Console.WriteLine("     Alla taggar är som följer: ");
				Tag TagsToDisplay = new Tag();
				TagsToDisplay.DisplayTags(Context);
			}
			else if (UserChoice == "4")
			{
				Post NewPost = new Post();
				NewPost.CreateNewPost(Context);
			}
			else if (UserChoice == "5")
			{
				Category NewCategory = new Category();
				NewCategory.CreateNewCategory(Context);
			}
			else if (UserChoice == "6")
			{
				Tag NewTag = new Tag();
				NewTag.CreateNewTag(Context);
			}
			else if (UserChoice == "7")
			{
				Post PostsToFind = new Post();
				PostsToFind.DisplayPostsOfOneCategory(Context);
			}
			else if (UserChoice == "8")
			{
				Post PostsToFind = new Post();
				PostsToFind.DisplayPostsOfOneTag(Context);
			}
			else if (UserChoice == "9")
			{
				Post PostToAdd = new Post();
				PostToAdd.AddPostToCategory(Context);
			}
			else if (UserChoice == "10")
			{
				Post PostToTag = new Post();
				PostToTag.AddPostToTag(Context);
			}
			else if (UserChoice == "q" || UserChoice == "Q")
			{
				Loop = false;
			}
			Console.ResetColor();
			Console.WriteLine("= = = = = = = = = = = = = = = =");
			Console.WriteLine();
		}
		
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Tack för att du använder applikationen.");
		Console.WriteLine();
		Console.WriteLine("Nästa gång kan du se följande Posts:");
		Console.ForegroundColor = ConsoleColor.Green;
		Post Posts = new Post();
		Posts.DisplayPosts(Context);
		Console.WriteLine();

		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Några av dessa Posts har organiserats under specifika kategorier:");
		Console.ForegroundColor = ConsoleColor.Green;
		Category Categorys = new Category();
		Categorys.DisplayCategories(Context);
		Console.WriteLine();

		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Några av Posts har taggats enligt följande:");
		Console.ForegroundColor = ConsoleColor.Green;
		Tag Tags = new Tag();
		Tags.DisplayTags(Context);
		Console.WriteLine();
		Console.ResetColor();


	}

}


