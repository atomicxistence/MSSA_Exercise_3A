using System;
using TaskrLibrary;

namespace TaskrConsole
{
	public class TaskrConsoleManager
	{
		private Taskr taskr = new Taskr();
		private Display display = new Display();
		private Input input = new Input();
		private Selection currentSelection = new Selection(0, 0);
		private Page currentPage;

		private bool forceRefresh;

		public void Run()
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			display.Initialize(taskr.GetPage(currentSelection.PageIndex));

			while (true)
			{
				var nextPage = taskr.GetPage(currentSelection.PageIndex);
				if (currentPage != nextPage)
				{
					currentSelection = new Selection(0, 0);
					currentPage = nextPage;
				}

				display.Refresh(currentPage, currentSelection, forceRefresh);
				forceRefresh = false;

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
			int totalItems = taskr.GetPage(currentSelection.PageIndex).Tasks.Count - 1;
			int totalPages = taskr.GetTotalPageCount() - 1;

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
						currentSelection = new Selection(0, 0);
					}
					else
					{
						currentSelection = new Selection(0, currentSelection.PageIndex + 1); ;
					}
					break;
				case InputType.PreviousPage:
					if (currentSelection.PageIndex == 0)
					{
						currentSelection = new Selection(0, totalPages);
					}
					else
					{
						currentSelection = new Selection(0, currentSelection.PageIndex - 1); ;
					}
					break;
				case InputType.Select:
					TaskSubMenu();
					forceRefresh = true;
					break;
				case InputType.NewTask:
					CreateNewTask();
					forceRefresh = true;
					break;
				case InputType.Quit:
					if (QuitSubMenu())
					{
						taskr.SaveTaskList();
						Environment.Exit(0);
					}
					break;
				case InputType.Back:
					forceRefresh = true;
					break;
				case InputType.Invalid:
					break;
			}
		}

		private void TaskSubMenu()
		{
			var taskMenuAction = InputType.Invalid;
			var subMenuSelection = new Selection(0, 0);
			var taskSubMenu = new Menu(MenuType.TaskMenu);
			var task = currentPage.Tasks[currentSelection.ItemIndex];
			var taskPrompt = "Select an option...";
			bool isUsing = true;

			display.SubMenuCompleteRefresh(taskSubMenu, subMenuSelection, taskPrompt);

			while (isUsing)
			{
				do
				{
					display.Refresh(currentPage, currentSelection, forceRefresh);
					display.SubMenuRefresh(taskSubMenu, subMenuSelection);
					taskMenuAction = input.Selection(SelectionType.TaskActionSelection);
				} while (taskMenuAction == InputType.Invalid);

				switch (taskMenuAction)
				{
					case InputType.NextItem:
						if (subMenuSelection.ItemIndex == taskSubMenu.Items.Count - 1)
						{
							subMenuSelection = new Selection(0, 0);
						}
						else
						{
							subMenuSelection = new Selection(subMenuSelection.ItemIndex + 1, 0);
						}
						break;
					case InputType.PreviousItem:
						if (subMenuSelection.ItemIndex == 0)
						{
							subMenuSelection = new Selection(taskSubMenu.Items.Count - 1, 0);
						}
						else
						{
							subMenuSelection = new Selection(subMenuSelection.ItemIndex - 1, 0);
						}
						break;
					case InputType.Select:
						switch (taskSubMenu.Items[subMenuSelection.ItemIndex].Action)
						{
							case OptionType.ActionTask:
								task.Actioned();
								taskr.CopyTaskToEndOfList(task);
								forceRefresh = true;
								isUsing = false;
								break;
							case OptionType.CompleteTask:
								if (!task.IsActioned)
								{
									task.Actioned();
								}
								task.Completed();
								forceRefresh = true;
								isUsing = false;
								break;
							case OptionType.Back:
								forceRefresh = true;
								isUsing = false;
								break;
							default:
								break;
						}
						break;
					case InputType.Back:
						forceRefresh = true;
						isUsing = false;
						break;
					case InputType.Invalid:
						break;
				}
			}

		}

		private bool QuitSubMenu()
		{
			var quitMenuAction = InputType.Invalid;
			var quitSelection = new Selection(0, 0);
			var quitMenu = new Menu(MenuType.YesNoMenu);
			var quitPrompt = "Are you sure you want to quit?";

			display.SubMenuCompleteRefresh(quitMenu, quitSelection, quitPrompt);

			while (true)
			{
				do
				{
					display.Refresh(currentPage, currentSelection, forceRefresh);
					display.SubMenuRefresh(quitMenu, quitSelection);
					quitMenuAction = input.Selection(SelectionType.YesNoSubSelection);
				} while (quitMenuAction == InputType.Invalid);

				switch (quitMenuAction)
				{
					case InputType.NextItem:
						if (quitSelection.ItemIndex == quitMenu.Items.Count - 1)
						{
							quitSelection = new Selection(0, 0);
						}
						else
						{
							quitSelection = new Selection(quitSelection.ItemIndex + 1, 0);
						}
						break;
					case InputType.PreviousItem:
						if (quitSelection.ItemIndex == 0)
						{
							quitSelection = new Selection(quitMenu.Items.Count - 1, 0);
						}
						else
						{
							quitSelection = new Selection(quitSelection.ItemIndex - 1, 0);
						}
						break;
					case InputType.Select:
						forceRefresh = true;
						return quitMenu.Items[quitSelection.ItemIndex].Action == OptionType.Yes;
					case InputType.Invalid:
						break;
				}
			}

		}

		private void CreateNewTask()
		{
			display.NewTaskEntry();
			Console.CursorVisible = true;
			var input = Console.ReadLine();
			Console.CursorVisible = false;
			if (!string.IsNullOrWhiteSpace(input))
			{
				taskr.AddTask(input);
			}
		}
	}
}
