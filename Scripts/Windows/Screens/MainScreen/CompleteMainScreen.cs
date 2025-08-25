using TwoOneTwoGames.UIManager.Components.Interactive;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class CompleteMainScreen : MainScreen
    {
        [Header("Buttons")]
        [SerializeField]
        private UIButton _settingsButton;
        [SerializeField]
        private UIButton _startButton;
        [SerializeField]
        private UIButton _levelsButton;

        // Injected
        private IMainScreenViewModel _viewModel;

        [Inject]
        public void Initialize(IMainScreenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Resume()
        {
            base.Resume();
            
            if (_levelsButton != null)
            {
                _viewModel.LevelsButton.Subscribe(_levelsButton.SetData);
            }
            if (_settingsButton != null)
            {
                _viewModel.SettingsButton.Subscribe(_settingsButton.SetData);
            }
            if (_startButton != null)
            {
                _viewModel.PlayButton.Subscribe(_startButton.SetData);
            }
            
            _viewModel.ScreenResumed();
        }

        public override void Hide()
        {
            base.Hide();
            
            if (_levelsButton != null)
            {
                _viewModel.LevelsButton.Unsubscribe(_levelsButton.SetData);
            }
            if (_settingsButton != null)
            {
                _viewModel.SettingsButton.Unsubscribe(_settingsButton.SetData);
            }
            if (_startButton != null)
            {
                _viewModel.PlayButton.Unsubscribe(_startButton.SetData);
            }
            _viewModel.ScreenHidden();
        }
    }
}