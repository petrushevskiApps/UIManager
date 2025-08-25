using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButton))]
public class SettingsButton : MonoBehaviour
{
    private UIButton _uiButton;
    private IPopupNavigation _popupNavigation;

    [Inject]
    private void Initialize(IPopupNavigation popupNavigation)
    {
        _popupNavigation = popupNavigation;
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
        _popupNavigation.ShowSettingsPopup();
    }
}
