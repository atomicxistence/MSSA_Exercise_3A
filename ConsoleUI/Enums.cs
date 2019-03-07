namespace TaskrConsole
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
		NewTask,
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

	public enum OptionType
	{
		Yes,
		No,
		ActionTask,
		CompleteTask,
		Back,
	}
}
