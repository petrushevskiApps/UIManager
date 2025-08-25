using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public interface IInGameScreenViewModel
    {
        IReactiveProperty<string> LevelTitle { get; }
        IReactiveProperty<UIProgressBarData> ProgressBarData { get; }
        IReactiveProperty<UIButtonViewData> PauseButton { get; }
        void ScreenResumed();
        void ScreenHidden();
    }
}