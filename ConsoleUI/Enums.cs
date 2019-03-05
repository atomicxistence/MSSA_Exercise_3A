namespace ConsoleUI
{
	public enum MenuType
	{
		TaskMenu,
		YesNoMenu,
	}

	public enum InputType
	{
		NextItem,
		PreviousItem,
		NextPage,
		PreviousPage,
		Select,
		Quit,
		Back,
		Invalid,
	}

	public enum SelectionType
	{
		TaskPageSelection,
		TaskActionSelection,
		YesNoSubSelection,
	}
}
