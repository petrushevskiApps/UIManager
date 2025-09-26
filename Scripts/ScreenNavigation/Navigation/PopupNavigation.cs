using TwoOneTwoGames.UIManager.Windows;
using TwoOneTwoGames.UIManager.Windows.Popups;
using TwoOneTwoGames.UIManager.Windows.TutorialPopup;
using TwoOneTwoGames.ZenRings.UserInterface.Windows;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class PopupNavigation : IPopupNavigation
    {
        private readonly INavigationController _navigationController;

        public PopupNavigation(INavigationController navigationController)
        {
            _navigationController = navigationController;
        }

        public void ShowNoInternetPopup()
        {
            _navigationController.ShowPopup<NoInternetPopup>();
        }

        public void ShowPausePopup()
        {
            _navigationController.ShowPopup<PausePopup>();
        }

        public void ShowExitLevelPopup()
        {
            _navigationController.ShowPopup<ExitLevelPopup>();
        }

        public void ShowSettingsPopup()
        {
            _navigationController.ShowPopup<SettingsPopup>();
        }

        public void ShowExitGamePopup()
        {
            _navigationController.ShowPopup<ExitGamePopup>();
        }
        
        public void ShowIconMessagePopup(IconMessagePopupArguments args)
        {
            _navigationController.ShowPopup<IconMessagePopup, IconMessagePopupArguments>(args);
        }
        public void ShowBuyBoosterPopup(BuyBoosterPopupArguments args)
        {
            _navigationController.ShowPopup<BuyBoosterPopup, BuyBoosterPopupArguments>(args);
        }
        public void ShowTutorialPopup(TutorialPopupArguments args)
        {
            _navigationController.ShowPopup<TutorialPopup, TutorialPopupArguments>(args);
        }
    }
}