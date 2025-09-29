using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public interface IPopupViewModel
    {
        IReactiveProperty<string> Title { get; }
        IReactiveProperty<string> Message { get; }
        void BackgroundClicked();
        void CloseClicked();
    }
}