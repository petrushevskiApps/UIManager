using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    internal class ExitLevelPopupViewModel : IExitLevelPopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; }
        public IReactiveProperty<string> Message { get; }
        public IReactiveProperty<UIButtonViewData> ConfirmButton { get; }
        public IReactiveProperty<UIButtonViewData> DiscardButton { get; }

        // Injected
        private readonly INavigationController _navigationController;
        private readonly IScreenNavigation _screenNavigation;
        private readonly IUILevelController _uiLevelController;
        private readonly IUiAnalyticsEventsHandler _analyticsEventsHandler;

        public ExitLevelPopupViewModel(
            INavigationController navigationController,
            IScreenNavigation screenNavigation,
            IUILevelController uiLevelController,
            IUiAnalyticsEventsHandler analyticsEventsHandler)
        {
            _navigationController = navigationController;
            _screenNavigation = screenNavigation;
            _uiLevelController = uiLevelController;
            _analyticsEventsHandler = analyticsEventsHandler;

            Title = new ReactiveProperty<string>("Exit Level");
            Message = new ReactiveProperty<string>("Are you sure? \nYour progress will be lost.");

            ConfirmButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                label: new TextViewData(true, "Yes"),
                clickAction: ExitLevel));
            DiscardButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                label: new TextViewData(true, "No"),
                clickAction: DiscardPopupClicked));
        }

        public void BackgroundClicked()
        {
            _navigationController.GoBack();
        }

        public void CloseClicked()
        {
            _navigationController.GoBack();
        }

        public void ExitLevel()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "YesButton");
            _uiLevelController.LeaveLevel();
        }

        public void DiscardPopupClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "NoButton");
            _navigationController.GoBack();
        }
    }
}