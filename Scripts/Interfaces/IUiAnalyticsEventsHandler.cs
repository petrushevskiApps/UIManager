namespace TwoOneTwoGames.UIManager.Interfaces
{
    public interface IUiAnalyticsEventsHandler
    {
        void SendUiEvent(string playerAction, string objectOfAction, float value = -1f);
    }
}