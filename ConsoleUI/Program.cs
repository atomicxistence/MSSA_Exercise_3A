using System;

namespace ConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			TaskrConsoleManager tcm = new TaskrConsoleManager();
			tcm.Run();
		}
	}
}
