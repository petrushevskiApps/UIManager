using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class ExitGamePopupViewModel : IExitGamePopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; protected set; }
        public IReactiveProperty<string> Message { get; protected set; }
        public IReactiveProperty<UIButtonViewData> ConfirmButton { get; }
        public IReactiveProperty<UIButtonViewData> DiscardButton { get; }

        // Injected
        private readonly INavigationController _navigationController;
        private readonly IExitAppController _exitAppController;
        private readonly IUiAnalyticsEventsHandler _analyticsEventsHandler;

        public ExitGamePopupViewModel(
            INavigationController navigationController,
            IExitAppController exitAppController,
            IUiAnalyticsEventsHandler analyticsEventsHandler)
        {
            _navigationController = navigationController;
            _exitAppController = exitAppController;
            _analyticsEventsHandler = analyticsEventsHandler;

            ConfirmButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                label: new TextViewData(true, "Yes"),
                clickAction: ExitApp));
            DiscardButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                label: new TextViewData(true, "No"),
                clickAction: DiscardPopupClicked));
        }

        public void BackgroundClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "BackgroundButton");
            _navigationController.GoBack();
        }

        public void CloseClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "CloseButton");
            _navigationController.GoBack();
        }

        private void DiscardPopupClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "NoButton");
            _navigationController.GoBack();
        }

        private void ExitApp()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "YesButton");
            _exitAppController.ExitApp();
        }
    }
}