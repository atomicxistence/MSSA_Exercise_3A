using System;
using TaskrConsoleUI.Common;
using TaskrLibrary;
using TaskrLibrary.FileIO;

namespace TaskrConsoleUI.Input
{
    public class InputManager
    {
        private InputSelection Selector { get;}
        private InputAction Actioner { get;}
        private SelectionState state;

        public InputManager(IFileTransaction fileTrans)
        {
            Selector = new InputSelection();
            Actioner = new InputAction(new Action<SelectionState>(ChangeState), fileTrans);
            state = SelectionState.MainSelection;
        }

        public ActionType GetAction(ConsoleKey input) =>
            Selector.Selection(input, state);

        public ActionResult PerformAction(ActionType action) =>
            Actioner.ActionInput(action, state);

        public ActionResult IntialPage() =>
            Actioner.CurrentActionResult;

        private void ChangeState(SelectionState newState)
        {
            state = newState;
        }
    }
}