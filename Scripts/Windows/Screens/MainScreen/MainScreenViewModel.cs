using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class MainScreenViewModel : IMainScreenViewModel
    {
        // Reactive Properties
        public IReactiveProperty<UIButtonViewData> PlayButton { get; }
        public IReactiveProperty<UIButtonViewData> LevelsButton { get; }
        public IReactiveProperty<UIButtonViewData> SettingsButton { get; }
        
        // Injected
        private readonly IScreenNavigation _screenNavigation;
        private readonly IPopupNavigation _popupNavigation;
        private readonly IUILevelController _uiLevelController;
        private readonly IBackgroundMusicAudioPalette _musicAudioPalette;
        private readonly IUiSoundSystem _uiSoundSystem;

        public MainScreenViewModel(
            IScreenNavigation screenNavigation,
            IPopupNavigation popupNavigation,
            IUILevelController uiLevelController,
            IBackgroundMusicAudioPalette musicAudioPalette,
            IUiSoundSystem uiSoundSystem)
        {
            _screenNavigation = screenNavigation;
            _popupNavigation = popupNavigation;
            _uiLevelController = uiLevelController;
            _musicAudioPalette = musicAudioPalette;
            _uiSoundSystem = uiSoundSystem;
            
            PlayButton = new ReactiveProperty<UIButtonViewData>(new UIButtonViewData(isVisible: false));
            LevelsButton = new ReactiveProperty<UIButtonViewData>(
                new UIButtonViewData(
                    label: new TextViewData(true, "Levels"),
                    clickAction: LevelsButtonClicked));
            SettingsButton = new ReactiveProperty<UIButtonViewData>(
                new UIButtonViewData(isVisible: true, clickAction: SettingsClicked));
        }

        public void ScreenResumed()
        {
            _uiLevelController.LevelReadyEvent += OnLevelReady;
            
            _uiSoundSystem.PlayBackgroundMusic(_musicAudioPalette.MainScreenBackgroundMusic);
            _uiLevelController.SetLastUnlockedLevel();
        }
        public void ScreenHidden()
        {
            _uiLevelController.LevelReadyEvent -= OnLevelReady;
        }
        
        private void OnLevelReady(string levelTitle)
        {
            PlayButton.Value = new UIButtonViewData(
                label: new TextViewData(true, $"Play Level {levelTitle}"),
                clickAction: StartLevelClicked);
        }

        private void LevelsButtonClicked()
        {
            _screenNavigation.ShowLevelsScreen();
        }
        
        protected virtual void SettingsClicked()
        {
            _popupNavigation.ShowSettingsPopup();
        }

        protected virtual void StartLevelClicked()
        {
            _uiLevelController.StartLevel();
        }
    }
}