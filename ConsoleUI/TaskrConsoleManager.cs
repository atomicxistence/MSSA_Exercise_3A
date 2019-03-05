using TaskTracker;

namespace ConsoleUI
{
	public class TaskrConsoleManager
	{
		private Taskr taskr = new Taskr();
		private Display display = new Display();
		private Input input = new Input();
		private Selection currentSelection = new Selection(itemIndex: 0, pageIndex: 0);

		public void Run()
		{
			while (true)
			{
				var currentPage = taskr.GetPage(currentSelection.PageIndex);
				display.Refresh(currentPage, currentSelection);

				InputType action = InputType.Invalid;
				do
				{
					action = input.Selection();
				} while (action == InputType.Invalid);

				ActionUserInput(action);

				// user task selection
				// task selection submenu
				// next page of tasks
				// exit & save
			}
		}

		private void ActionUserInput(InputType action)
		{
			int totalItems = taskr.GetPage(currentSelection.PageIndex).Tasks.Count;
			int totalPages = taskr.GetTotalPageCount();

			switch (action)
			{
				case InputType.NextItem:
					if (currentSelection.ItemIndex == totalItems)
					{
						currentSelection = new Selection(0, currentSelection.PageIndex);
					}
					else
					{
						currentSelection = new Selection(currentSelection.ItemIndex + 1, currentSelection.PageIndex); ;
					}
					break;
				case InputType.PreviousItem:
					if (currentSelection.ItemIndex == 0)
					{
						currentSelection = new Selection(totalItems, currentSelection.PageIndex);
					}
					else
					{
						currentSelection = new Selection(currentSelection.ItemIndex - 1, currentSelection.PageIndex); ;
					}
					break;
				case InputType.NextPage:
					if (currentSelection.PageIndex == totalPages)
					{
						currentSelection = new Selection(currentSelection.ItemIndex, 0);
					}
					else
					{
						currentSelection = new Selection(currentSelection.ItemIndex, currentSelection.PageIndex + 1); ;
					}
					break;
				case InputType.PreviousPage:
					if (currentSelection.PageIndex == 0)
					{
						currentSelection = new Selection(currentSelection.ItemIndex, totalPages);
					}
					else
					{
						currentSelection = new Selection(currentSelection.ItemIndex, currentSelection.PageIndex - 1); ;
					}
					break;
				case InputType.Select:
					break;
				case InputType.Quit:
					break;
				case InputType.Invalid:
					break;
				default:
					break;
			}
		}
	}
}
