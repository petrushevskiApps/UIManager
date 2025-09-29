using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class NoInternetPopupViewModel : INoInternetPopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; protected set; }
        public IReactiveProperty<string> Message { get; protected set; }
        public IReactiveProperty<UIButtonViewData> OkButton { get; }
        
        // Injected
        private readonly INavigationController _navigationController;

        public NoInternetPopupViewModel(INavigationController navigationController)
        {
            _navigationController = navigationController;
            Title = new ReactiveProperty<string>("Network connection failed.");
            Message = new ReactiveProperty<string>("Please check your cellular or Wi-Fi connection and retry.");
            
            OkButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                label: new TextViewData(true, "Ok"),
                clickAction: OkButtonClicked));
        }
        
        public void BackgroundClicked()
        {
            _navigationController.GoBack();
        }

        public void CloseClicked()
        {
            _navigationController.GoBack();
        }

        public void OkButtonClicked()
        {
            _navigationController.GoBack();
        }
    }
}