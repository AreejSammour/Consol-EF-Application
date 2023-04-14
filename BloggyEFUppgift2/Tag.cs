using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggyEFUppgift2
{
	internal class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Post> Posts { get; set; }

		public void DisplayTags(BloggyDbContext context)
		{
			Console.WriteLine();
			var TagsList = context.Tags.ToList();
			foreach (var tag in TagsList)
			{
				Console.WriteLine("       " + tag.Id + ") " + tag.Name);
				Console.WriteLine("       -------------------");
				Console.WriteLine();
			}
		}

		public void CreateNewTag(BloggyDbContext context)
		{
			Console.Write("Ange det nya taggnamnet: ");
			var NewTagName = Console.ReadLine();
			Console.WriteLine();

			Tag NewTag = new Tag();
			NewTag.Name = NewTagName;

			// Kolla om det inte har lagts till tidigare
			var TagsList = context.Tags.ToList();
			bool TagExist = false;
			foreach (var item in TagsList)
			{
				if (item.Name == NewTag.Name)
				{
					Console.WriteLine();
					Console.WriteLine("     Det är ingen ny tagg !!.. Det finns redan där.");
					Console.WriteLine();
					TagExist = true;

				}
			}
			if (TagExist == false)
			{
				context.Tags.Add(NewTag);
				context.SaveChanges();

				Console.WriteLine();
				Console.WriteLine("     >>> Den nya taggen har lagts till.");
				Console.WriteLine();
			}
		}
	}
}
