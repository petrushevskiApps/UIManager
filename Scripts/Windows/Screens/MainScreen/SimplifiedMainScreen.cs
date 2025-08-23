using System.Collections;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.Windows;
using Zenject;

public class SimplifiedMainScreen : MainScreen
{
    private IUiSoundSystem _uiSoundSystem;
    private IBackgroundMusicAudioPalette _musicAudioPalette;

    [Inject]
    public void Initialize(
        IUiSoundSystem uiSoundSystem,
        IBackgroundMusicAudioPalette musicAudioPalette)
    {
        _uiSoundSystem = uiSoundSystem;
        _musicAudioPalette = musicAudioPalette;
    }

    public override void Resume()
    {
        base.Resume();
        _uiSoundSystem.PlayBackgroundMusic(_musicAudioPalette.MainScreenBackgroundMusic);
    }
}