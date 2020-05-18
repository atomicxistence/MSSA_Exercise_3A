using System;
using TaskrLibrary;
using TaskrLibrary.Models;
using TaskrLibrary.FileIO;
using TaskrConsoleUI.Input;
using TaskrConsoleUI.Common;

namespace TaskrConsoleUI
{
    public class App
    {
        private readonly IFileTransaction fileTrans;

        public App(IFileTransaction fileTrans)
        {
            this.fileTrans = fileTrans;
        }
        public void Run()
        {
            var input = new InputManager(fileTrans);
            var actionResult = input.IntialPage();

            //main loop
            while (true)
            {
                while(!Console.KeyAvailable)
                {
                    //pass actionResult to display
                    //refresh display
                }
                //get user input
                var keyPressed = Console.ReadKey(true).Key;
                ActionType desiredAction = input.GetAction(keyPressed);
                //action user input if the key pressed was valid
                if (desiredAction != ActionType.Invalid) 
                {
                    actionResult = input.PerformAction(desiredAction);
                }
            }
        }
    }
}