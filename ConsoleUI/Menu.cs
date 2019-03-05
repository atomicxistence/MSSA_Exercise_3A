using System.Collections.Generic;

namespace ConsoleUI
{
	public enum MenuType
	{
		TaskMenu,
		YesNoMenu,
	}

	public class Menu
	{
		public List<Option> Items { get; private set; }

		public Menu(MenuType type)
		{
			switch (type)
			{
				case MenuType.TaskMenu:
					Items.Add(new Option("Action This Task", 0));
					Items.Add(new Option("Complete This Task", 1));
					Items.Add(new Option("Back", 2));
					break;
				case MenuType.YesNoMenu:
					Items.Add(new Option("Yes", 0));
					Items.Add(new Option("No", 1));
					break;
			}
		}
	}
}
