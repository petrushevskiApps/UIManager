using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public interface ILevelFailedScreenViewModel
    {
        void OnBackTriggered();
        void ScreenShown();
        void ScreenHidden();
        void ScreenClosed();
        IReactiveProperty<UIButtonViewData> ReviveButton { get; }
        IReactiveProperty<UIButtonViewData> ReplayButton { get; }
        IReactiveProperty<UIButtonViewData> HomeButton { get; }
        IReactiveProperty<UIButtonViewData> SettingsButton { get; }
        IReactiveProperty<string> Title { get; }
        void SetReviveButtonTitle(bool isVisible, string title = null, Action onReviveAction = null);
    }
}