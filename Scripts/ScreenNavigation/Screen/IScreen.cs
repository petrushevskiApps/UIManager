namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    /// <summary>
    ///     This interface represents screens which can be
    ///     controlled by the <see cref="NavigationController" />,
    ///     and contains screen lifecycle methods.
    /// </summary>
    public interface IScreen : IBackHandler
    {
        string ScreenTitle { get; }
        
        bool IsPopup { get; }

        /// <summary>
        ///     <c>True: if this screen should be added on backstack,</c>
        ///     <c>False: if this screen should not be added on backstack</c>
        /// </summary>
        bool IsBackStackable { get; }

        /// <summary>
        ///     Screen Lifecycle method, Invoked by <see cref="NavigationController" />
        ///     and used for setting and showing the screen.
        /// </summary>
        /// <param name="navArguments">Navigational arguments passed by invoking screens.</param>
        void Show<TArguments>(TArguments navArguments);

        /// <summary>
        ///     Screen Lifecycle method, invoked by <see cref="NavigationController" />
        ///     when another screen is pushed on the backstack.
        ///     <remarks>
        ///         Use this screen to hide / disable the visuals / view of this screen.
        ///     </remarks>
        /// </summary>
        void Hide();

        /// <summary>
        ///     Screen Lifecycle method, invoked by <see cref="NavigationController" />
        ///     when the screen is re-shown from the backstack.
        ///     <remarks>
        ///         Use this method to show / enable the visuals / view of this screen.
        ///     </remarks>
        /// </summary>
        void Resume();

        /// <summary>
        ///     Screen Lifecycle method, invoked by <see cref="NavigationController" />
        ///     when the screen is removed from the backstack.
        ///     <remarks>
        ///         Use this method to clear all data which requires cleaning when this
        ///         screen is removed from backstack.
        ///     </remarks>
        /// </summary>
        void Close();
    }
}