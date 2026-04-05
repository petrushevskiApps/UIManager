using System;

namespace TwoOneTwoGames.UIManager.Interfaces
{
    public interface IUILevelController
    {
        event Action<string> LevelReadyEvent;
        event Action<string> LevelStartedEvent;
        void StartLevel(int funnelId, int levelId);
        void StartLevel();
        void RestartLevel();
        void ReviveAndContinueLevel();
        void CollectReward(int earnedStars);
        void CollectDoubleReward(int earnedStars);
        void LeaveLevel();
        void StartNextLevel();
        void SetLastUnlockedLevel();
    }
}