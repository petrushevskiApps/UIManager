using System;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class PageLoadedEventArguments : EventArgs
    {
        public int ElementsInPage { get; }

        public int ScrollToElementIndex { get; }

        public PageLoadedEventArguments(int scrollToElementIndex, int elementsInPage)
        {
            ScrollToElementIndex = scrollToElementIndex;
            ElementsInPage = elementsInPage;
        }
    }
}