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
        private readonly IFileTransaction fileTrans;
        private Page page;
        private SelectionState currentState;

        public UiManager(IFileTransaction fileTrans)
        {
            this.fileTrans = fileTrans;
            currentState = SelectionState.MainSelection;
        }
        public void Run()
        {
            var taskr = new Taskr(fileTrans);
            page = taskr.CurrentPage;

            var input = new InputManager();

            //main loop
            while (true)
            {
                while(!Console.KeyAvailable)
                {
                    //refresh display

                }
                //get user input
                var keyPressed = Console.ReadKey(true).Key;
                ActionType desiredAction = input.GetAction(keyPressed, currentState);
                //action user input
                Page newPage = input.PerformAction(desiredAction);

            }
        }
    }
}