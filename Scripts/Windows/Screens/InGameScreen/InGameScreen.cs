using TwoOneTwoGames.UIManager.ScreenNavigation;

namespace TwoOneTwoGames.UIManager.Windows
{
    public abstract class InGameScreen : UIScreen
    {
        public void PauseClicked()
        {
            PopupNavigation.ShowPausePopup();
        }
        public override void OnBackTriggered()
        {
            PopupNavigation.ShowExitLevelPopup();
        }
    }
}