using TaskrLibrary.Models;

namespace TaskrConsoleUI.Common
{
    public struct ActionResult
    {
        public Page Page { get;}
        public int Selection { get;}

        public ActionResult(Page page, int selection)
        {
            Page = page;
            Selection = selection;
        }
    }
}