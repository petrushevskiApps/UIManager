using System;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public interface INavigationController
    {
        /// <summary>
        ///     Hides or Closes the currently active screen on backstack
        ///     and shows screen of type T passing the navigational arguments
        ///     provided.
        /// </summary>
        /// <typeparam name="T">Type of screen to be shown.</typeparam>
        /// <typeparam name="TArguments">Arguments to be passed in Show method of the screen.</typeparam>
        void ShowScreen<T, TArguments>(TArguments navArguments) where T : IScreen;

        void ShowScreen<T>() where T : IScreen;

        /// <summary>
        ///     Closes and removes currently active screen (top-stack screen) from the backstack,
        ///     and Resumes the next screen available on the stack.
        /// </summary>
        void GoBack();

        void ShowPopup<T, TArguments>(TArguments navArguments) where T : IScreen;
        void ShowPopup<T>() where T : IScreen;
        event EventHandler AllScreensClosedEvent;
        IBackHandler GetActiveBackHandler();
        void ClearAllStackScreens();
        float GetActiveScreenTime();
    }
}