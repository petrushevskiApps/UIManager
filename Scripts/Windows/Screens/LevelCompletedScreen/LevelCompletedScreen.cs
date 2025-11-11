using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class LevelCompletedScreen : UIScreen
    {
        private IUiHapticsController _uiHapticsController;
        
        [Inject]
        private void InitializeLevelCompletedScreen(
            IUiHapticsController uiHapticsController)
        {
            _uiHapticsController = uiHapticsController;
        }
        
        public override void Show<TArguments>(TArguments navArguments)
        {
            base.Show(navArguments);
            _uiHapticsController.LevelCompleted();
        }

        public override void OnBackTriggered()
        {
            ScreenNavigation.ShowMainScreen();
        }
    }
}