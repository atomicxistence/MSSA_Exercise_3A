using System.Collections.Generic;

namespace ConsoleUI
{
	public class Menu
	{
		public List<Option> Items { get; private set; }

		public Menu(MenuType type)
		{
			Items = new List<Option>();

			switch (type)
			{
				case MenuType.TaskMenu:
					Items.Add(new Option("Action This Task", OptionType.ActionTask));
					Items.Add(new Option("Complete This Task", OptionType.CompleteTask));
					Items.Add(new Option("Back", OptionType.Back));
					break;
				case MenuType.YesNoMenu:
					Items.Add(new Option("Yes", OptionType.Yes));
					Items.Add(new Option("No", OptionType.No));
					break;
			}
		}
	}
}
