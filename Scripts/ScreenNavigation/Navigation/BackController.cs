using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class BackController : MonoBehaviour
    {
        [Inject]
        private INavigationController _navigationController;

        [Inject]
        private IUiAnalyticsEventsHandler _analyticsEventsHandler;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "Back");
                _navigationController.GetActiveBackHandler()?.OnBackTriggered();
            }
        }
    }
}