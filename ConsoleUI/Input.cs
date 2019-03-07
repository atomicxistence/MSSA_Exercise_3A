using System;

namespace TaskrConsole
{
	public class Input
	{
		public InputType Selection(SelectionType selectionType)
		{
			var input = Console.ReadKey(true);

			switch (selectionType)
			{
				case SelectionType.TaskPageSelection:
					return TaskSelect(input.Key);
				case SelectionType.TaskActionSelection:
					return ActionSelect(input.Key);
				case SelectionType.YesNoSubSelection:
					return YesNoSelect(input.Key);
				default:
					return InputType.Invalid;
			}
		}

		private InputType TaskSelect(ConsoleKey input)
		{
			switch (input)
			{
				case ConsoleKey.Enter:
					return InputType.Select;
				case ConsoleKey.N:
					return InputType.NewTask;
				case ConsoleKey.UpArrow:
					return InputType.PreviousItem;
				case ConsoleKey.DownArrow:
					return InputType.NextItem;
				case ConsoleKey.RightArrow:
					return InputType.NextPage;
				case ConsoleKey.LeftArrow:
					return InputType.PreviousPage;
				case ConsoleKey.Escape:
					return InputType.Quit;
				default:
					return InputType.Invalid;
			}
		}

		private InputType ActionSelect(ConsoleKey input)
		{
			switch (input)
			{
				case ConsoleKey.Enter:
					return InputType.Select;
				case ConsoleKey.UpArrow:
					return InputType.PreviousItem;
				case ConsoleKey.DownArrow:
					return InputType.NextItem;
				case ConsoleKey.Escape:
					return InputType.Back;
				default:
					return InputType.Invalid;
			}
		}

		private InputType YesNoSelect(ConsoleKey input)
		{
			switch (input)
			{
				case ConsoleKey.Enter:
					return InputType.Select;
				case ConsoleKey.UpArrow:
					return InputType.PreviousItem;
				case ConsoleKey.DownArrow:
					return InputType.NextItem;
				default:
					return InputType.Invalid;
			}
		}
	}
}
