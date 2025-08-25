using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class SimplifiedInGameScreen: InGameScreen
    {
        private IUiSoundSystem _uiSoundSystem;
        private IBackgroundMusicAudioPalette _musicAudioPalette;

        [Inject]
        public void InitializeSimplifiedInGameScreen(
            IUiSoundSystem uiSoundSystem,
            IBackgroundMusicAudioPalette musicAudioPalette)
        {
            _uiSoundSystem = uiSoundSystem;
            _musicAudioPalette = musicAudioPalette;
        }
        
        public override void Resume()
        {
            base.Resume();
            _uiSoundSystem.PlayBackgroundMusic(_musicAudioPalette.InGameBackgroundMusic, isInMenu: false);
        }
    }
}