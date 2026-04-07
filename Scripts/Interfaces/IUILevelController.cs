using System;

namespace TwoOneTwoGames.UIManager.Interfaces
{
    public interface IUILevelController
    {
        event Action<string> LevelReadyEvent;
        event Action<string> LevelStartedEvent;
        
        /// <summary>
        /// Level with provided World Id and Level Id was selected.
        /// </summary>
        /// <param name="worldId">World Id for the selected level.</param>
        /// <param name="levelId">Level id for the selected level.</param>
        void LevelSelected(int worldId, int levelId);
        
        /// <summary>
        /// Start last unlocked level in the currently loaded funnel.
        /// </summary>
        void StartLastUnlockedLevel();
        
        /// <summary>
        /// Star the next level sequentially following the currently
        /// active level.
        /// </summary>
        void StartNextLevel();
        
        /// <summary>
        /// Player clicked continue button on Level Completed Screen.
        /// </summary>
        void LevelCompletedContinueClicked();
        
        /// <summary>
        /// Player clicked continue button on Level Failed Screen.
        /// </summary>
        void LevelFailedContinueClicked();
        
        /// <summary>
        /// Restart the currently played level.
        /// </summary>
        void RestartLevel();
        
        /// <summary>
        /// Revive and allow the player to continue playing where
        /// he has stopped.
        /// </summary>
        void ReviveAndContinueLevel();
        
        /// <summary>
        /// Player chose to leave / exit the level.
        /// </summary>
        void LeaveLevel();
        
        /// <summary>
        /// Collect level reward was chosen by the player.
        /// </summary>
        void CollectLevelReward();
        
        /// <summary>
        /// Collect double level reward was chosen by the player.
        /// </summary>
        void CollectDoubleReward(int earnedStars);
    }
}