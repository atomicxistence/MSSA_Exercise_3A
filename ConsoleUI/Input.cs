using System;

namespace ConsoleUI
{
	public class Input
	{

		public int ListSelection(int totalItems)
		{
			int selectionItem = 0;
			bool isSelecting = true;

			do
			{
				var input = Console.ReadKey();

				switch (input.Key)
				{
					case ConsoleKey.Enter:
						isSelecting = false;
						break;
					case ConsoleKey.UpArrow:
						if (selectionItem == 0)
						{
							selectionItem = totalItems - 1;
						}
						else
						{
							selectionItem -= 1;
						}
						isSelecting = true;
						break;
					case ConsoleKey.DownArrow:
						if (selectionItem == totalItems - 1)
						{
							selectionItem = 0;
						}
						else
						{
							selectionItem += 1;
						}
						isSelecting = true;
						break;
				}

			} while (isSelecting);

			return selectionItem;
		}
	}
}
