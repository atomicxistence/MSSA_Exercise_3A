using System;
using TaskrLibrary;
using TaskrLibrary.Models;
using TaskrLibrary.FileIO;
using TaskrConsoleUI.Input;
using TaskrConsoleUI.Common;

namespace TaskrConsoleUI
{
    public class UiManager
    {
        private readonly Taskr taskr;
        private Page currentPage;

        public UiManager(IFileTransaction fileTrans)
        {
            taskr = new Taskr(fileTrans);
        }
        public void Run()
        {
            //main loop
            while (true)
            {
                //get user input
                //action user input
                //refresh display
            }
        }
    }
}