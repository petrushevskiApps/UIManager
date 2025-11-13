using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.Interactive
{
    public class PauseButton : MonoBehaviour
    {
        private UIButton _uiButton;
        private IPopupNavigation _popupNavigation;
        private IUiAnalyticsEventsHandler _analyticsEventsHandler;

        [Inject]
        private void Initialize(
            IUiAnalyticsEventsHandler analyticsEventsHandler,
            IPopupNavigation popupNavigation)
        {
            _analyticsEventsHandler = analyticsEventsHandler;
            _popupNavigation = popupNavigation;
        }
        private void Awake()
        {
            _uiButton = GetComponent<UIButton>();
            _uiButton.SetData(new UIButtonViewData(
                isVisible: true, 
                clickAction: Clicked));
        }

        private void Clicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "PauseButton");
            _popupNavigation.ShowPausePopup();
        }
    }
}