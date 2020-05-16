using System;
using TaskrConsoleUI.Common;
using TaskrLibrary.Models;

namespace TaskrConsoleUI.Input
{
    public class InputManager
    {
        private InputSelection Selector { get;}
        private InputAction Actioner { get;}
        public InputManager()
        {
            Selector = new InputSelection();
            Actioner = new InputAction();
        }

        public ActionType GetAction(ConsoleKey input, SelectionState state) =>
            Selector.Selection(input, state);

        public Page PerformAction(ActionType action) =>
            Actioner.ActionInput(action);
}