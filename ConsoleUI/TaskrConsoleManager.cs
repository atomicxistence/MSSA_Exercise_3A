using System;
using TaskTracker;

namespace ConsoleUI
{
	public class TaskrConsoleManager
	{
		private Taskr taskr = new Taskr();
		private Display display = new Display();
		private Input input = new Input();
		private Selection currentSelection = new Selection(itemIndex: 0, pageIndex: 0);
		private Page currentPage;

		public void Run()
		{
			while (true)
			{
				currentPage = taskr.GetPage(currentSelection.PageIndex);
				display.Refresh(currentPage, currentSelection);

				InputType action = InputType.Invalid;
				do
				{
					action = input.Selection(SelectionType.TaskPageSelection);
				} while (action == InputType.Invalid);

				ActionUserTaskListInput(action);
			}
		}

		private void ActionUserTaskListInput(InputType action)
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
					TaskSubMenu(taskr.GetPage(currentSelection.PageIndex).Tasks[currentSelection.ItemIndex]);
					break;
				case InputType.Quit:
					if (QuitSubMenu())
					{
						taskr.SaveTaskList();
						Environment.Exit(0);
					}
					break;
				case InputType.Back:
					break;
				case InputType.Invalid:
					break;
				default:
					break;
			}
		}

		private void TaskSubMenu(Task task)
		{
			var taskMenuAction = InputType.Invalid;
			var subSelection = new Selection(0, 0);
			var taskSubMenu = new Menu(MenuType.TaskMenu);

			while (true)
			{
				do
				{
					display.Refresh(currentPage, currentSelection);
					display.SubMenuRefresh(taskSubMenu, subSelection);
					taskMenuAction = input.Selection(SelectionType.TaskActionSelection);
				} while (taskMenuAction == InputType.Invalid);

				switch (taskMenuAction)
				{
					case InputType.NextItem:
						if (subSelection.ItemIndex == taskSubMenu.Items.Count)
						{
							subSelection = new Selection(0, 0);
						}
						else
						{
							subSelection = new Selection(subSelection.ItemIndex + 1, 0);
						}
						break;
					case InputType.PreviousItem:
						if (subSelection.ItemIndex == 0)
						{
							subSelection = new Selection(taskSubMenu.Items.Count, 0);
						}
						else
						{
							subSelection = new Selection(subSelection.ItemIndex - 1, 0);
						}
						break;
					case InputType.Select:
						switch (taskSubMenu.Items[subSelection.ItemIndex].Action)
						{
							case OptionType.ActionTask:
								currentPage.Tasks[currentSelection.ItemIndex].Actioned();
								break;
							case OptionType.CompleteTask:
								currentPage.Tasks[currentSelection.ItemIndex].Completed();
								break;
							case OptionType.Back:
								break;
							default:
								break;
						}
						break;
					case InputType.Back:
						break;
					case InputType.Invalid:
						break;
				}
			}

		}

		private bool QuitSubMenu()
		{
			var quitMenuAction = InputType.Invalid;
			var subSelection = new Selection(0, 0);
			var quitMenu = new Menu(MenuType.YesNoMenu);

			while (true)
			{
				do
				{
					display.Refresh(currentPage, currentSelection);
					display.SubMenuRefresh(quitMenu, subSelection);
					quitMenuAction = input.Selection(SelectionType.YesNoSubSelection);
				} while (quitMenuAction == InputType.Invalid);

				switch (quitMenuAction)
				{
					case InputType.NextItem:
						if (subSelection.ItemIndex == quitMenu.Items.Count)
						{
							subSelection = new Selection(0, 0);
						}
						else
						{
							subSelection = new Selection(subSelection.ItemIndex + 1, 0);
						}
						break;
					case InputType.PreviousItem:
						if (subSelection.ItemIndex == 0)
						{
							subSelection = new Selection(quitMenu.Items.Count, 0);
						}
						else
						{
							subSelection = new Selection(subSelection.ItemIndex - 1, 0);
						}
						break;
					case InputType.Select:
						return quitMenu.Items[subSelection.ItemIndex].Action == OptionType.Yes;
					case InputType.Invalid:
						break;
				}
			}

		}
	}
}
