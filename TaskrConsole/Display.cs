using System;
using TaskrLibrary;

namespace TaskrConsole
{
	public class Display
	{
		#region DisplayVariables
		private int currentWindowWidth;
		private int currentWindowHeight;
		private int centeredWindowTop;
		private int centeredWindowLeft;
		private int promptOffset = 3;
		private int widthMin = 80;
		private int heightMin = Global.PageSize;
		private int pageTopOffset;
		private int pageLeftOffset = 2;
		private int subMenuVerticalOffset = 1;
		private int subMenuLeftOffset = 3;
		private int subMenuPromptOffset = 2;

		private string prompt = "▲ ▼ Tasks | ◄ ► Pages | N = New Task  | Esc = Quit";
		private string subMenuPrompt;

		private Selection previousSelection;
		private Selection nextSelection;
		private Page currentPage;
		private Selection currentSubSelection;
		private Selection nextSubSelection;

		private bool needSubMenuRefresh;
		private bool pageHasChanged;

		#region Colors
		private ConsoleColor colorSubMenuBG = ConsoleColor.DarkGray;
		private ConsoleColor colorSubMenuFG = ConsoleColor.White;
		private ConsoleColor colorTitleBG = ConsoleColor.DarkGray;
		private ConsoleColor colorTitleFG = ConsoleColor.Cyan;
		private ConsoleColor colorPromptFG = ConsoleColor.Cyan;
		private ConsoleColor colorPromptBG = ConsoleColor.DarkGray;
		private ConsoleColor colorTaskActioned = ConsoleColor.DarkGray;
		private ConsoleColor colorTaskSelectedBG = ConsoleColor.White;
		private ConsoleColor colorTaskSelectedFG = ConsoleColor.DarkYellow;
		private ConsoleColor colorTextEntryBG = ConsoleColor.White;
		private ConsoleColor colorTextEntryFG = ConsoleColor.Black;
		private ConsoleColor colorDefaultFG = ConsoleColor.Black;
		private ConsoleColor colorDefaultBG = ConsoleColor.Gray;
		#endregion

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
			heightMin += title.Length + promptOffset;
			SetInitialWindowSize();
			pageTopOffset = centeredWindowTop + title.Length + promptOffset;
			Console.CursorVisible = false;
			previousSelection = new Selection(0, 0);
			nextSelection = new Selection(0, 0);
			currentSubSelection = new Selection(0, 0);
			nextSubSelection = new Selection(0, 0);
			CompleteRefresh();
		}

		public void Refresh(Page currentPage, Selection nextSelection, bool forceRefresh)
		{
			pageHasChanged = this.currentPage != currentPage;
			this.currentPage = currentPage;
			this.nextSelection = nextSelection;

			Console.CursorVisible = false;

			if (WindowSizeHasChanged())
			{
				CompleteRefresh();
			}

			if (previousSelection.PageIndex != nextSelection.PageIndex)
			{
				PrintPageContents();
				PrintSelections();
				previousSelection = new Selection(nextSelection.ItemIndex, nextSelection.PageIndex);
			}

			if (previousSelection.ItemIndex != nextSelection.ItemIndex)
			{
				PrintSelections();
				previousSelection = new Selection(nextSelection.ItemIndex, nextSelection.PageIndex);
			}

			if (forceRefresh)
			{
				PrintPageContents();
				PrintSelections();
			}
		}

		public void SubMenuCompleteRefresh(Menu subMenu, Selection nextSubSelection, string subMenuPrompt)
		{
			this.nextSubSelection = nextSubSelection;
			currentSubSelection = nextSubSelection;

			this.subMenuPrompt = subMenuPrompt;

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
				SubMenuCompleteRefresh(subMenu, nextSubSelection, subMenuPrompt);
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
			var textEntryPrompt = "New Task: ";

			Console.BackgroundColor = colorSubMenuBG;
			Console.ForegroundColor = colorSubMenuFG;

			for (int i = 0; i < entryFieldVerticalSize; i++)
			{
				Console.SetCursorPosition(entryFieldLeftStart, entryFieldTopStart + i);
				PrintEmptySpaceFill(entryFieldHorizontalSize);
			}

			Console.SetCursorPosition(entryFieldLeftStart + subMenuLeftOffset, 
									  entryFieldTopStart + (entryFieldVerticalSize / 2));
			Console.Write(textEntryPrompt);

			// Print Text Entry Background
			Console.ForegroundColor = colorTextEntryFG;
			Console.BackgroundColor = colorTextEntryBG;
			PrintEmptySpaceFill(entryFieldHorizontalSize - textEntryPrompt.Length - (subMenuLeftOffset * 2));
			Console.SetCursorPosition(entryFieldLeftStart + subMenuLeftOffset + textEntryPrompt.Length + 1,
									  entryFieldTopStart + (entryFieldVerticalSize / 2));
		}

		//-----------------------------------------------------------------------------------------

		private void PrintSubMenuField(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			Console.ForegroundColor = colorSubMenuFG;
			Console.BackgroundColor = colorSubMenuBG;

			for (int i = 0; i < subMenu.Items.Count + (subMenuVerticalOffset * 2) + subMenuPromptOffset; i++)
			{
				Console.SetCursorPosition(subMenuLeftStart, subMenuTopStart + i);
				PrintEmptySpaceFill(widthMin / 2);
			}
		}

		private void PrintSubMenuOptions(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			Console.ForegroundColor = colorSubMenuFG;
			Console.BackgroundColor = colorSubMenuBG;

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
										  subMenuTopStart + subMenuVerticalOffset);
			Console.Write(subMenuPrompt);

			for (int i = 0; i < subMenu.Items.Count; i++)
			{
				Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
										  subMenuTopStart + subMenuVerticalOffset + subMenuPromptOffset + i);
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
												  subMenuTopStart + subMenuVerticalOffset + currentSubSelection.ItemIndex + subMenuPromptOffset);
			PrintEmptySpaceFill((widthMin / 2) - (subMenuLeftOffset * 2) + 1);

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
									  subMenuTopStart + subMenuVerticalOffset + currentSubSelection.ItemIndex + subMenuPromptOffset);
			Console.Write(subMenu.Items[currentSubSelection.ItemIndex].Title);
		}

		private void PrintSubMenuNextSelection(Menu subMenu, int subMenuLeftStart, int subMenuTopStart)
		{
			Console.ForegroundColor = colorTaskSelectedFG;
			Console.BackgroundColor = colorTaskSelectedBG;

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
									  subMenuTopStart + subMenuVerticalOffset + nextSubSelection.ItemIndex + subMenuPromptOffset);
			PrintEmptySpaceFill((widthMin / 2) - (subMenuLeftOffset * 2));

			Console.SetCursorPosition(subMenuLeftStart + subMenuLeftOffset,
									  subMenuTopStart + subMenuVerticalOffset + nextSubSelection.ItemIndex + subMenuPromptOffset);
			Console.Write(selectionIndicator);
			Console.Write(subMenu.Items[nextSubSelection.ItemIndex].Title);
			PrintEmptySpaceFill((widthMin / 2) - (subMenuLeftOffset * 2) -
								(subMenu.Items[nextSubSelection.ItemIndex].Title.Length +
								 selectionIndicator.Length));
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
			Console.ResetColor();
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
			Task previousTask;
			if (pageHasChanged)
			{
				previousTask = currentPage.Tasks[0];
			}
			else
			{
				previousTask = currentPage.Tasks[previousSelection.ItemIndex];
				PrintPreviousSelection(previousTask);
			}

			var nextTask = currentPage.Tasks[nextSelection.ItemIndex];
			PrintNextSelection(nextTask);
		}

		private void PrintPreviousSelection(Task currentTask)
		{
			Console.ForegroundColor = currentTask.IsActioned ? colorTaskActioned : colorDefaultFG;
			Console.BackgroundColor = colorDefaultBG;

			Console.SetCursorPosition(centeredWindowLeft,
									  centeredWindowTop + pageTopOffset + previousSelection.ItemIndex);
			PrintEmptySpaceFill(widthMin);
			
			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
									  centeredWindowTop + pageTopOffset + previousSelection.ItemIndex);
			Console.Write(currentTask.Title);
			// Print date created
			string dateCreated = currentTask.TimeStamp.ToShortDateString();
			Console.SetCursorPosition(centeredWindowLeft + widthMin - pageLeftOffset - dateCreated.Length,
									  centeredWindowTop + pageTopOffset + previousSelection.ItemIndex);
			Console.Write(dateCreated);
		}

		private void PrintNextSelection(Task nextTask)
		{
			Console.ForegroundColor = nextTask.IsActioned ? colorTaskActioned : colorTaskSelectedFG;
			Console.BackgroundColor = colorTaskSelectedBG;
			Console.SetCursorPosition(centeredWindowLeft,
									  centeredWindowTop + pageTopOffset + nextSelection.ItemIndex);
			Console.Write(selectionIndicator);
			Console.Write(nextTask.Title);
			PrintEmptySpaceFill(widthMin - nextTask.Title.Length - selectionIndicator.Length);
			// Print date created
			string dateCreated = nextTask.TimeStamp.ToShortDateString();
			Console.SetCursorPosition(centeredWindowLeft + widthMin - pageLeftOffset - dateCreated.Length,
									  centeredWindowTop + pageTopOffset + nextSelection.ItemIndex);
			Console.Write(dateCreated);
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
		}

		private void PrintPrompt()
		{
			Console.ForegroundColor = colorPromptFG;
			Console.BackgroundColor = colorPromptBG;

			// Print prompt background
			for (int i = 0; i < promptOffset - 1; i++)
			{
				Console.SetCursorPosition(centeredWindowLeft, centeredWindowTop + title.Length + i);
				PrintEmptySpaceFill(widthMin);
			}

			// Print prompt text
			Console.SetCursorPosition(centeredWindowLeft + ((widthMin / 2) - (prompt.Length / 2)), 
									  centeredWindowTop + title.Length);
			Console.Write(prompt);

			// Print background division before page contents
			Console.ForegroundColor = colorDefaultFG;
			Console.BackgroundColor = colorDefaultBG;
			Console.SetCursorPosition(centeredWindowLeft, centeredWindowTop + title.Length + promptOffset - 1);
			PrintEmptySpaceFill(widthMin);
		}

		private void PrintPageContents()
		{
			Console.ForegroundColor = colorDefaultFG;
			Console.BackgroundColor = colorDefaultBG;
			// Print page background
			for (int i = 0; i < Global.PageSize; i++)
			{
				Console.SetCursorPosition(centeredWindowLeft, centeredWindowTop + pageTopOffset + i);
				PrintEmptySpaceFill(widthMin);
			}
			// Print task list page contents
			for (int i = 0; i < currentPage.Tasks.Count; i++)
			{
				Console.ForegroundColor = colorDefaultFG;
				Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset, 
										  centeredWindowTop + pageTopOffset + i);
				Console.ForegroundColor = currentPage.Tasks[i].IsActioned ? colorTaskActioned : colorDefaultFG;
				Console.Write(currentPage.Tasks[i].Title);
				// Print date created
				string dateCreated = currentPage.Tasks[i].TimeStamp.ToShortDateString();
				Console.SetCursorPosition(centeredWindowLeft + widthMin - pageLeftOffset - dateCreated.Length,
										  centeredWindowTop + pageTopOffset + i);
				Console.Write(dateCreated);
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
