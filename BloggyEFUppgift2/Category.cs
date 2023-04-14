using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggyEFUppgift2
{
	internal class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Post> Posts { get; set; }

		public void DisplayCategories(BloggyDbContext context)
		{
			Console.WriteLine();
			var CategorysList = context.Categories.ToList();
			foreach (var Cat in CategorysList)
			{
				Console.WriteLine("       " + Cat.Id + ") " + Cat.Name);
				Console.WriteLine("       -------------------");
				Console.WriteLine();
			}
		}

		public void CreateNewCategory(BloggyDbContext context)
		{
			Console.Write("Ange det nya kategorinamnet: ");
			var NewCatName = Console.ReadLine();
			Console.WriteLine();

			Category NewCat = new Category();
			NewCat.Name = NewCatName;

			// Kolla om det inte har lagts till tidigare
			var CatsList = context.Categories.ToList();
			bool CatExist = false;
			foreach (var item in CatsList)
			{
				if (item.Name == NewCat.Name)
				{
					Console.WriteLine();
					Console.WriteLine("     Det är ingen ny kategori !!.. Det finns redan där.");
					Console.WriteLine();
					CatExist = true;
					
				}
			}
			if (CatExist == false)
			{
				context.Categories.Add(NewCat);
				context.SaveChanges();

				Console.WriteLine();
				Console.WriteLine("     >>> Den nya kategorin har lagts till.");
				Console.WriteLine();
			}
		}
	}
}
