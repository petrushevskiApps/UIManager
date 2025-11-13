using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButton))]
public class SettingsButton : MonoBehaviour
{
    private UIButton _uiButton;
    private IPopupNavigation _popupNavigation;
    private IUiAnalyticsEventsHandler _analyticsEventsHandler;

    [Inject]
    private void Initialize(
        IPopupNavigation popupNavigation,
        IUiAnalyticsEventsHandler analyticsEventsHandler)
    {
        _popupNavigation = popupNavigation;
        _analyticsEventsHandler = analyticsEventsHandler;
    }
    private void Awake()
    {
        _uiButton = GetComponent<UIButton>();
        _uiButton.SetData(new UIButtonViewData(
            isVisible: true, 
            clickAction: SettingsClicked));
    }

    private void SettingsClicked()
    {
        _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "SettingsButton");
        _popupNavigation.ShowSettingsPopup();
    }
}
