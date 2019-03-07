namespace TaskrConsole
{
	public struct Selection
	{
		public int ItemIndex { get; private set; }
		public int PageIndex { get; private set; }

		public Selection(int itemIndex, int pageIndex)
		{
			ItemIndex = itemIndex;
			PageIndex = pageIndex;
		}
	}
}
