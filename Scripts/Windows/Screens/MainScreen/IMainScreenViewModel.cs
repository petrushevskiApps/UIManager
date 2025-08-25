using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public interface IMainScreenViewModel
    {
        IReactiveProperty<UIButtonViewData> PlayButton { get; }
        IReactiveProperty<UIButtonViewData> LevelsButton { get; }
        IReactiveProperty<UIButtonViewData> SettingsButton { get; }
        void ScreenResumed();
        void ScreenHidden();
    }
}