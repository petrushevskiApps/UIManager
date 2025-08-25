using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public sealed class InGameScreenViewModel : IInGameScreenViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> LevelTitle { get; }
        public IReactiveProperty<UIProgressBarData> ProgressBarData { get; }
        public IReactiveProperty<UIButtonViewData> PauseButton { get; }

        // Injected
        private readonly IPopupNavigation _popupNavigation;
        private IUILevelController _uiLevelController;
        private readonly IBackgroundMusicAudioPalette _musicAudioPalette;
        private readonly IUiSoundSystem _uiSoundSystem;

        public InGameScreenViewModel(
            IPopupNavigation popupNavigation,
            IUILevelController uiLevelController,
            IBackgroundMusicAudioPalette musicAudioPalette,
            IUiSoundSystem uiSoundSystem)
        {
            _popupNavigation = popupNavigation;
            _uiLevelController = uiLevelController;
            _musicAudioPalette = musicAudioPalette;
            _uiSoundSystem = uiSoundSystem;

            LevelTitle = new ReactiveProperty<string>("");
            ProgressBarData = new ReactiveProperty<UIProgressBarData>();
            PauseButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(
                clickAction: PauseClicked));
        }

        public void ScreenResumed()
        {
            _uiSoundSystem.PlayBackgroundMusic(_musicAudioPalette.InGameBackgroundMusic, isInMenu: false);
            _uiLevelController.LevelStartedEvent += OnLevelStarted;
        }

        public void ScreenHidden()
        {
            _uiLevelController.LevelStartedEvent -= OnLevelStarted;
        }

        private void OnLevelStarted(object sender, string levelTitle)
        {
            LevelTitle.Value = $"Level {levelTitle}";
        }

        private void PauseClicked()
        {
            _popupNavigation.ShowPausePopup();
        }
    }
}