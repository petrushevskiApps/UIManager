using System;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using TwoOneTwoGames.UIManager.Windows;

namespace TwoOneTwoGames.UIManager.Components.Interactive.LevelsList
{
    public class LevelsListViewModel: ILevelsListViewModel
    {
        private const int PAGE_SIZE = 10;
    
        // Reactive Properties
        public event EventHandler<PageLoadedEventArguments> PageLoadedEvent;
        public IReactiveProperty<int> LastCompletedLevelIndex { get; }
        public List<IUILevelData> Levels { get; private set; } = new();

        // Injected
        private readonly ILevelsDataProvider _levelsDataProvider;
        private readonly IUILevelController _levelController;
        private bool _allPagesFetched;
        private int _currentPage;

        public LevelsListViewModel(
            IScreenNavigation screenNavigation,
            ILevelsDataProvider levelsDataProvider,
            IUILevelController levelController)
        {
            _levelsDataProvider = levelsDataProvider;
            _levelController = levelController;

            LastCompletedLevelIndex = new ReactiveProperty<int>();
        }
        public void Setup()
        {
            _currentPage = 0;
            _allPagesFetched = false;
            Levels.Clear();
        }

        public void LoadNextPage()
        {
            if (_allPagesFetched)
            {
                return;
            }

            List<IUILevelData> loadedPage = _levelsDataProvider.GetLevels(_currentPage, PAGE_SIZE);
            _currentPage++;
            if (loadedPage.Count < PAGE_SIZE)
            {
                _allPagesFetched = true;
            }

            Levels.AddRange(loadedPage);
            int indexOfLastCompletedLevel = GetIndexOfLevelToScroll();
            PageLoadedEvent?.Invoke(
                this, 
                new PageLoadedEventArguments(
                    Math.Clamp(indexOfLastCompletedLevel - 1, 0, Levels.Count), loadedPage.Count));
            if (indexOfLastCompletedLevel == -1)
            {
                LoadNextPage();
            }
        }

        public void OnLevelClicked(int funnelId, int levelId)
        {
            _levelController.LevelSelected(funnelId, levelId);
        }

        private int GetIndexOfLevelToScroll()
        {
            IUILevelData levelData = _levelsDataProvider.GetLastUnlockedLevel();
            if (levelData == null)
            {
                return -1;
            }
            return Levels.FindIndex(ld => levelData.Id == ld.Id);
        }
    }
}