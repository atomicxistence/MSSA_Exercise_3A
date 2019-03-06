using System;
using TaskTracker;

namespace ConsoleUI
{
	public class Display
	{
		#region DisplayVariables
		private int currentWindowWidth;
		private int currentWindowHeight;
		private int centeredWindowTop;
		private int centeredWindowLeft;
		private int widthMin = 80;
		private int heightMin = Global.PageSize + 3;
		private int pageTopOffset;
		private int pageLeftOffset = 2;
		private int subMenuVerticalOffset = 1;
		private int subMenuLeftOffset = 3;

		private string prompt = "▲ ▼ Tasks | ◄ ► Pages | N = New Task  | Esc = Quit";

		private Selection currentSelection;
		private Selection nextSelection;
		private Page currentPage;
		private Selection currentSubSelection;
		private Selection nextSubSelection;

		private bool needSubMenuRefresh;

		private ConsoleColor colorSubMenuBG = ConsoleColor.DarkGray;
		private ConsoleColor colorSubMenuFG = ConsoleColor.Black;
		private ConsoleColor colorTitleBG = ConsoleColor.DarkGray;
		private ConsoleColor colorTitleFG = ConsoleColor.Cyan;
		private ConsoleColor colorPromptFG = ConsoleColor.Cyan;
		private ConsoleColor colorTaskActioned = ConsoleColor.DarkGray;
		private ConsoleColor colorTaskSelectedBG = ConsoleColor.White;
		private ConsoleColor colorTaskSelectedFG = ConsoleColor.Black;

		private string selectionIndicator = " ► ";
		private string[] title = new string[]
			{"                                         ",
			 "████████╗ █████╗ ███████╗██╗  ██╗██████╗ ", 
			 "╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝██╔══██╗",
			 "   ██║   ███████║███████╗█████╔╝ ██████╔╝",
			 "   ██║   ██╔══██║╚════██║██╔═██╗ ██╔══██╗",
			 "   ██║   ██║  ██║███████║██║  ██╗██║  ██║",
			 "   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝",
			 "                                         "};
		#endregion

		public void Initialize(Page currentPage)
		{
			this.currentPage = currentPage;
			heightMin += title.Length;
			SetInitialWindowSize();
			pageTopOffset = centeredWindowTop + title.Length + 2;
			Console.CursorVisible = false;
			currentSelection = new Selection(0, 0);
			nextSelection = new Selection(0, 0);
			currentSubSelection = new Selection(0, 0);
			nextSubSelection = new Selection(0, 0);
			CompleteRefresh();
		}

		public void Refresh(Page currentPage, Selection nextSelection, bool menuHasClosed)
		{
			this.currentPage = currentPage;
			this.nextSelection = nextSelection;

			Console.CursorVisible = false;

			if (WindowSizeHasChanged())
			{
				CompleteRefresh();
			}

			if (currentSelection.ItemIndex != nextSelection.ItemIndex)
			{
				PrintSelections();
				currentSelection = new Selection(nextSelection.ItemIndex, nextSelection.PageIndex);
			}

			if (currentSelection.PageIndex != nextSelection.PageIndex)
			{
				PrintPageContents();
				PrintSelections();
				currentSelection = new Selection(nextSelection.ItemIndex, nextSelection.PageIndex);
			}

			if (menuHasClosed)
			{
				PrintPageContents();
				PrintSelections();
			}
		}

		public void SubMenuCompleteRefresh(Menu subMenu, Selection nextSubSelection)
		{
			this.nextSubSelection = nextSubSelection;
			currentSubSelection = nextSubSelection;

			var subMenuHorizontalSize = widthMin / 2;
			var subMenuLeftStart = (Console.WindowWidth / 2)  - (subMenuHorizontalSize / 2);
			var subMenuTopStart = (Console.WindowHeight / 2) - (subMenu.Items.Count / 2) + (subMenuVerticalOffset * 2);

			PrintSubMenuField(subMenu, subMenuLeftStart, subMenuTopStart);
			PrintSubMenuOptions(subMenu, subMenuLeftStart, subMenuTopStart);
			PrintSubMenuSelections(subMenu);
		}

		public void SubMenuRefresh(Menu subMenu, Selection nextSubSelection)
		{
			this.nextSubSelection = nextSubSelection;

			if (needSubMenuRefresh)
			{
				SubMenuCompleteRefresh(subMenu, nextSubSelection);
			}

			if (currentSubSelection.ItemIndex != nextSubSelection.ItemIndex)
			{
				PrintSubMenuSelections(subMenu);
				currentSubSelection = new Selection(nextSubSelection.ItemIndex, 0);
			}
		}

		public void NewTaskEntry()
		{
			var entryFieldHorizontalSize = widthMin / 2 + widthMin / 4;
			var entryFieldVerticalSize = 3;
			var entryFieldLeftStart = (Console.WindowWidth / 2) - (entryFieldHorizontalSize / 2);
			var entryFieldTopStart = (Console.WindowHeight / 2) - (entryFieldVerticalSize / 2);

			Console.BackgroundColor = colorSubMenuBG;
			Console.ForegroundColor = colorSubMenuFG;

			for (int i = 0; i < entryFieldVerticalSize; i++)
			{
				Console.SetCursorPosition(entryFieldLeftStart, entryFieldTopStart + i);
				PrintEmptySpaceFill(entryFieldHorizontalSize);
			}

			Console.SetCursorPosition(entryFieldLeftStart + subMenuLeftOffset, 
									  entryFieldTopStart + (entryFieldVerticalSize / 2));
			Console.Write("New Task: ");
		}

		private void PrintSubMenuField(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			Console.ForegroundColor = colorSubMenuFG;
			Console.BackgroundColor = colorSubMenuBG;

			for (int i = 0; i < subMenu.Items.Count + (subMenuVerticalOffset * 2); i++)
			{
				Console.SetCursorPosition(subMenuLeftStart, subMenuTopStart + i);
				PrintEmptySpaceFill(widthMin / 2);
			}
		}

		private void PrintSubMenuOptions(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			for (int i = 0; i < subMenu.Items.Count; i++)
			{
				Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
										  subMenuTopStart + subMenuVerticalOffset + i);
				Console.Write(subMenu.Items[i].Title);
			}
		}

		private void PrintSubMenuSelections(Menu subMenu)
		{
			var subMenuHorizontalSize = widthMin / 2;
			var subMenuLeftStart = (Console.WindowWidth / 2) - (subMenuHorizontalSize / 2);
			var subMenuTopStart = (Console.WindowHeight / 2) - (subMenu.Items.Count / 2) + (subMenuVerticalOffset * 2);

			PrintSubMenuPreviousSelection(subMenu, subMenuLeftStart, subMenuTopStart);
			PrintSubMenuNextSelection(subMenu, subMenuLeftStart, subMenuTopStart);
		}

		private void PrintSubMenuPreviousSelection(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			Console.ForegroundColor = colorSubMenuFG;
			Console.BackgroundColor = colorSubMenuBG;

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
												  subMenuTopStart + subMenuVerticalOffset + currentSubSelection.ItemIndex);
			PrintEmptySpaceFill((widthMin / 2) - (subMenuLeftOffset * 2) + 1);

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
									  subMenuTopStart + subMenuVerticalOffset + currentSubSelection.ItemIndex);
			Console.Write(subMenu.Items[currentSubSelection.ItemIndex].Title);

			Console.ResetColor();
		}

		private void PrintSubMenuNextSelection(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			Console.ForegroundColor = colorTaskSelectedFG;
			Console.BackgroundColor = colorTaskSelectedBG;

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
									  subMenuTopStart + subMenuVerticalOffset + nextSubSelection.ItemIndex);
			PrintEmptySpaceFill((widthMin / 2) - (subMenuLeftOffset * 2));

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
									  subMenuTopStart + subMenuVerticalOffset + nextSubSelection.ItemIndex);
			Console.Write(selectionIndicator);
			Console.Write(subMenu.Items[nextSubSelection.ItemIndex].Title);
			PrintEmptySpaceFill((widthMin / 2) - (subMenuLeftOffset * 2) -
								(subMenu.Items[nextSubSelection.ItemIndex].Title.Length +
								 selectionIndicator.Length));
			Console.ResetColor();
		}

		private void SetInitialWindowSize()
		{
			Console.SetWindowSize(widthMin, heightMin);
			currentWindowWidth = Console.WindowWidth;
			currentWindowHeight = Console.WindowHeight;

			SetCenteredWindowEdges();
		}

		private void SetCenteredWindowEdges()
		{
			centeredWindowLeft = (currentWindowWidth / 2) - (widthMin / 2);
			centeredWindowTop = (currentWindowHeight / 2) - (heightMin / 2);
		}

		private bool WindowSizeHasChanged()
		{
			bool hasChanged = false;

			while (Console.WindowWidth < widthMin || Console.WindowHeight < heightMin)
			{
				hasChanged = true;
				Console.Clear();
				Console.WriteLine($"Please adjust your window size to at least 80 by {heightMin}. Press any key to continue.");
				Console.ReadKey();
			}

			if (Console.WindowWidth != currentWindowWidth || Console.WindowHeight != currentWindowHeight)
			{
				hasChanged = true;
				currentWindowWidth = Console.WindowWidth;
				currentWindowHeight = Console.WindowHeight;
			}

			needSubMenuRefresh = hasChanged;

			return hasChanged;
		}

		private void CompleteRefresh()
		{
			Console.Clear();
			Console.CursorVisible = false;
			SetCenteredWindowEdges();
			PrintTitle();
			PrintPrompt();
			PrintPageContents();
			PrintSelections();
		}

		private void PrintSelections()
		{
			var currentTask = currentPage.Tasks[currentSelection.ItemIndex];
			var nextTask = currentPage.Tasks[nextSelection.ItemIndex];

			PrintCurrentSelection(currentTask);
			PrintNextSelection(nextTask);
		}

		private void PrintCurrentSelection(Task currentTask)
		{
			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
									  centeredWindowTop + pageTopOffset + currentSelection.ItemIndex);
			PrintEmptySpaceFill(widthMin - pageLeftOffset);

			if (currentTask.IsActioned)
			{
				Console.ForegroundColor = colorTaskActioned;
			}

			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
									  centeredWindowTop + pageTopOffset + currentSelection.ItemIndex);
			Console.Write(currentTask.Title);
		}

		private void PrintNextSelection(Task nextTask)
		{
			Console.ForegroundColor = nextTask.IsActioned ? colorTaskActioned : colorTaskSelectedFG;
			Console.BackgroundColor = colorTaskSelectedBG;
			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
									  centeredWindowTop + pageTopOffset + nextSelection.ItemIndex);
			Console.Write($"{selectionIndicator}{nextTask.Title}");
			PrintEmptySpaceFill(widthMin - pageLeftOffset - nextTask.Title.Length - selectionIndicator.Length);
			Console.ResetColor();
		}

		private void PrintTitle()
		{
			Console.SetCursorPosition(centeredWindowLeft, centeredWindowTop);
			Console.ForegroundColor = colorTitleFG;
			Console.BackgroundColor = colorTitleBG;

			// Write Background Color
			for (int i = 0; i <= title.Length; i++)
			{
				PrintEmptySpaceFill(widthMin);
				Console.SetCursorPosition(centeredWindowLeft, centeredWindowTop + i);
			}
			// Write Centered Title
			for (int i = 0; i < title.Length; i++)
			{
				Console.SetCursorPosition(centeredWindowLeft + ((widthMin / 2) - (title[i].Length / 2)), centeredWindowTop + i);
				Console.Write(title[i]);
			}
			Console.ResetColor();
		}

		private void PrintPrompt()
		{
			Console.ForegroundColor = colorPromptFG;
			Console.SetCursorPosition(centeredWindowLeft + ((widthMin / 2) - (prompt.Length / 2)), centeredWindowTop + title.Length);
			Console.Write(prompt);
			Console.ResetColor();
		}

		private void PrintPageContents()
		{
			for (int i = 0; i < currentPage.Tasks.Count; i++)
			{
				PrintEmptySpaceFill(widthMin);
			}

			for (int i = 0; i < currentPage.Tasks.Count; i++)
			{
				Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset, centeredWindowTop + pageTopOffset + i);
				Console.Write(currentPage.Tasks[i].Title);
			}
		}

		private void PrintEmptySpaceFill(int emptySpace)
		{
			for (int i = 0; i < emptySpace; i++)
			{
				Console.Write(" ");
			}
		}
	}
}
