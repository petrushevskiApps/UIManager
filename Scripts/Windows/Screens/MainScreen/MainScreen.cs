using TwoOneTwoGames.UIManager.ScreenNavigation;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class MainScreen : UIScreen
    {
        public override void OnBackTriggered()
        {
            PopupNavigation.ShowExitGamePopup();
        }
    }
}