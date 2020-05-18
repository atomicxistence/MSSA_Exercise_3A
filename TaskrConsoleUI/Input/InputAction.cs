using System;
using TaskrLibrary;
using TaskrConsoleUI.Common;
using TaskrLibrary.Models;
using TaskrLibrary.FileIO;
using System.Collections.Generic;

namespace TaskrConsoleUI.Input
{
    public class InputAction
    {
        public ActionResult CurrentActionResult => new ActionResult(taskr.CurrentPage, rowNumber);
        private int rowNumber;
        private Stack<int> previousRows;
        private Action<SelectionState> callback;
        private Taskr taskr;

        public InputAction(Action<SelectionState> callback, IFileTransaction fileTrans)
        {
            this.callback = callback;
            this.taskr = new Taskr(fileTrans);
            rowNumber = 0;
            previousRows = new Stack<int>();
        }
        public ActionResult ActionInput(ActionType action, SelectionState state) =>
            state switch
            {
                SelectionState.MainSelection => ActionMain(action),
                SelectionState.SubMenuSelection => ActionSubMenu(action),
                SelectionState.ConfirmationSelection => ActionConfirmation(action),
                _=> throw new InvalidOperationException($"{state} is not a valid SelectionState")
            };

        private void ChangeState(SelectionState newState)
        {
            callback(newState);
        }

        private ActionResult ActionMain(ActionType action) =>
            action switch
            {
                ActionType.Select =>        SelectMain(),
                ActionType.NewTask =>       CreateNewTask(),
                ActionType.NextItem =>      SelectNextItem(),
                ActionType.PreviousItem =>  SelectPreviousItem(),
                ActionType.NextPage =>      SelectNextPage(),
                ActionType.PreviousPage =>  SelectPreviousPage(),
                ActionType.Quit =>          SelectQuit(),
                _=>                         throw new InvalidOperationException($"{action} is not a vaild ActionType")
            };

        private ActionResult ActionSubMenu(ActionType action)
        {

        }

        private ActionResult ActionNewTask(ActionType action)
        {

        }

        private ActionResult ActionConfirmation(ActionType action)
        {

        }

        private ActionResult SelectMain()
        {
            if (!taskr.CurrentPage.Tasks[rowNumber].IsActioned)
            {
                ChangeState(SelectionState.SubMenuSelection);
                previousRows.Push(rowNumber);
                rowNumber = 0;
            }
            return CurrentActionResult;
        } 

        private ActionResult CreateNewTask()
        {
            // how do i trigger the display.NewTaskEntry() here?
            throw new NotImplementedException();
        }

        private ActionResult SelectNextItem()
        {
            if (rowNumber >= taskr.CurrentPage.Tasks.Count - 1)
            {
                rowNumber = 0;
                return new ActionResult(taskr.NextPage, rowNumber);
            }
            else
            {
                rowNumber++;
                return CurrentActionResult;
            }
        }

        private ActionResult SelectPreviousItem()
        {
            if (rowNumber <= taskr.CurrentPage.Tasks.Count - 1)
            {
                var page = taskr.PreviousPage;
                rowNumber = page.Tasks.Count - 1;
                return CurrentActionResult;
            }
            else
            {
                rowNumber--;
                return CurrentActionResult;
            }
        }

        private ActionResult SelectNextPage()
        {
            var page = taskr.NextPage;
            var rowMax =  page.Tasks.Count - 1;
            rowNumber = rowNumber > rowMax ? rowMax : rowNumber;
            return CurrentActionResult;
        }

        private ActionResult SelectPreviousPage()
        {
            var page = taskr.PreviousPage;
            var rowMax =  page.Tasks.Count - 1;
            rowNumber = rowNumber > rowMax ? rowMax : rowNumber;
            return CurrentActionResult;
        }

        private ActionResult SelectQuit()
        {
            throw new NotImplementedException();
        } 
    }
}