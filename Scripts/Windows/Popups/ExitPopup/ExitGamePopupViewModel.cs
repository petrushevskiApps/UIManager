using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
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

        public ExitGamePopupViewModel(
            INavigationController navigationController,
            IExitAppController exitAppController)
        {
            _navigationController = navigationController;
            _exitAppController = exitAppController;

            ConfirmButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                label: new TextViewData(true, "Yes"),
                clickAction: ExitApp));
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

        private void DiscardPopupClicked()
        {
            _navigationController.GoBack();
        }

        private void ExitApp()
        {
            _exitAppController.ExitApp();
        }
    }
}