using TwoOneTwoGames.UIManager.Windows.Popups;
using TwoOneTwoGames.UIManager.Windows.TutorialPopup;
using TwoOneTwoGames.ZenRings.UserInterface.Windows;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public interface IPopupNavigation
    {
        public void ShowNoInternetPopup();
        public void ShowPausePopup();
        public void ShowExitLevelPopup();
        public void ShowSettingsPopup();
        public void ShowExitGamePopup();
        public void ShowIconMessagePopup(IconMessagePopupArguments args);
        void ShowBuyBoosterPopup(BuyBoosterPopupArguments args);
        void ShowTutorialPopup(TutorialPopupArguments args);
    }
}