using TMPro;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class FullLevelCompletedScreen : UIScreen
    {
        [SerializeField]
        private UIStars _stars;

        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private TextMeshProUGUI _earnedCoinsText;

        [Header("Buttons")]
        [SerializeField]
        private UIButton _replayButton;

        [SerializeField]
        private UIButton _homeButton;

        [SerializeField]
        private UIButton _settingsButton;

        [SerializeField]
        private UIButton _nextButton;

        [SerializeField]
        private UIButton _doubleRewardButton;

        private IUiHapticsController _uiHapticsController;

        // Injected
        protected ILevelCompletedScreenViewModel ViewModel;

        [Inject]
        private void Initialize(
            ILevelCompletedScreenViewModel viewModel,
            IUiHapticsController uiHapticsController)
        {
            ViewModel = viewModel;
            _uiHapticsController = uiHapticsController;
        }

        public override void Show<TArguments>(TArguments navArguments)
        {
            base.Show(navArguments);
            if (navArguments is LevelCompletedArguments arguments)
            {
                ViewModel.SetEarnedPoints(arguments.EarnedPoints);
                ViewModel.SetEarnedStars(arguments.EarnedStars);
                _uiHapticsController.LevelCompleted();
            }
        }

        public override void Resume()
        {
            base.Resume();
            ViewModel.ScreenResumed();

            if (_replayButton != null)
            {
                ViewModel.ReplayButton.Subscribe(_replayButton.SetData);
            }

            if (_homeButton != null)
            {
                ViewModel.HomeButton.Subscribe(_homeButton.SetData);
            }

            if (_settingsButton != null)
            {
                ViewModel.SettingsButton.Subscribe(_settingsButton.SetData);
            }

            if (_nextButton != null)
            {
                ViewModel.NextButton.Subscribe(_nextButton.SetData);
            }

            if (_doubleRewardButton != null)
            {
                ViewModel.DoubleRewardButton.Subscribe(_doubleRewardButton.SetData);
            }

            if (_stars != null)
            {
                ViewModel.EarnedStars.Subscribe(_stars.SetData);
            }

            if (_title != null)
            {
                ViewModel.Title.Subscribe(_title.SetData);
            }
            if (_earnedCoinsText != null)
            {
                ViewModel.EarnedCoinsText.Subscribe(_earnedCoinsText.SetData);
            }
        }

        public override void Hide()
        {
            base.Hide();
            
            if (_replayButton != null)
            {
                ViewModel.ReplayButton.Unsubscribe(_replayButton.SetData);
            }

            if (_homeButton != null)
            {
                ViewModel.HomeButton.Unsubscribe(_homeButton.SetData);
            }

            if (_settingsButton != null)
            {
                ViewModel.SettingsButton.Unsubscribe(_settingsButton.SetData);
            }

            if (_nextButton != null)
            {
                ViewModel.NextButton.Unsubscribe(_nextButton.SetData);
            }

            if (_doubleRewardButton != null)
            {
                ViewModel.DoubleRewardButton.Unsubscribe(_doubleRewardButton.SetData);
            }

            if (_stars != null)
            {
                ViewModel.EarnedStars.Unsubscribe(_stars.SetData);
            }

            if (_title != null)
            {
                ViewModel.Title.Unsubscribe(_title.SetData);
            }
            if (_earnedCoinsText != null)
            {
                ViewModel.EarnedCoinsText.Unsubscribe(_earnedCoinsText.SetData);
            }
            ViewModel.ScreenHidden();
        }

        public override void Close()
        {
            base.Close();
            ViewModel.ScreenClosed();
        }

        public override void OnBackTriggered()
        {
            ViewModel.OnBackTriggered();
        }
    }
}