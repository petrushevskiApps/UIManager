using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.Interactive
{
    public class PauseButton : MonoBehaviour
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
                clickAction: Clicked));
        }

        private void Clicked()
        {
            _popupNavigation.ShowPausePopup();
        }
    }
}