using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class SimplifiedLevelFailedScreen : LevelFailedScreen
    {
        // Internal
        private bool _isSfxPlayed;

        // Injected
        private IUiSoundSystem _uiSoundSystem;
        private IBackgroundMusicAudioPalette _musicAudioPalette;
        private IUiAudioPalette _uiAudioPalette;

        [Inject]
        public void InitializeSimplifiedLevelCompletedScreen(
            IUiSoundSystem uiSoundSystem,
            IBackgroundMusicAudioPalette musicAudioPalette,
            IUiAudioPalette uiAudioPalette)
        {
            _uiSoundSystem = uiSoundSystem;
            _musicAudioPalette = musicAudioPalette;
            _uiAudioPalette = uiAudioPalette;
        }
        
        public override void Resume()
        {
            Resume();
            if (!_isSfxPlayed)
            {
                _uiSoundSystem.PlayUiSoundEffect(_uiAudioPalette.LevelFailedBackgroundMusic);
                _isSfxPlayed = true;
            }
            _uiSoundSystem.PlayBackgroundMusic(_musicAudioPalette.MainScreenBackgroundMusic);
        }

        public override void Hide()
        {
            Hide();
            _uiSoundSystem.StopBackgroundMusic();
        }

        public override void Close()
        {
            Close();
            _isSfxPlayed = false;
        }
    }
}