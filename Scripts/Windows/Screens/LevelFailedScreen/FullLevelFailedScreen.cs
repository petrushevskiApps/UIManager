using TMPro;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class FullLevelFailedScreen : LevelFailedScreen
    {
        [SerializeField]
        private TextMeshProUGUI _title;
        
        [SerializeField]
        private UIButton _reviveButton;

        [SerializeField]
        private UIButton _replayButton;

        [SerializeField]
        private UIButton _homeButton;

        [SerializeField]
        private UIButton _settingsButton;

        // Injected
        protected ILevelFailedScreenViewModel ViewModel;

        [Inject]
        private void Initialize(ILevelFailedScreenViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override void Resume()
        {
            base.Resume();

            if (_title != null)
            {
                ViewModel.Title.Subscribe(_title.SetData);
            }

            if (_reviveButton != null)
            {
                ViewModel.ReviveButton.Subscribe(_reviveButton.SetData);
            }

            if (_replayButton != null)
            {
                ViewModel.ReplayButton.Subscribe(_replayButton.SetData);
            }

            if (_replayButton != null)
            {
                ViewModel.HomeButton.Subscribe(_homeButton.SetData);
            }

            if (_settingsButton != null)
            {
                ViewModel.SettingsButton.Subscribe(_settingsButton.SetData);
            }
            
            ViewModel.ScreenShown();
        }

        public override void Hide()
        {
            base.Hide();
            
            if (_title != null)
            {
                ViewModel.Title.Unsubscribe(_title.SetData);
            }

            if (_reviveButton != null)
            {
                ViewModel.ReviveButton.Unsubscribe(_reviveButton.SetData);
            }

            if (_replayButton != null)
            {
                ViewModel.ReplayButton.Unsubscribe(_replayButton.SetData);
            }

            if (_replayButton != null)
            {
                ViewModel.HomeButton.Unsubscribe(_homeButton.SetData);
            }

            if (_settingsButton != null)
            {
                ViewModel.SettingsButton.Unsubscribe(_settingsButton.SetData);
            }

            ViewModel.ScreenHidden();
        }

        public override void Close()
        {
            base.Close();
            ViewModel.ScreenClosed();
        }
    }
}