using System;
using TaskrLibrary.FileIO;

namespace TaskrConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.CursorVisible = false;

            var fileTrans = new SqliteTransaction();

            new UiManager(fileTrans).Run();
        }
    }
}
