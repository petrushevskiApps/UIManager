using System;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.BaseGame.Audio
{
    /// <summary>
    /// Adds additional sound effects and background music
    /// on top of the default background music and sound effects
    /// played for every screen, handled in the UI Popup.
    /// </summary>
    [RequireComponent(typeof(UIScreen))]
    public class ScreenCustomSoundAdder : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _shownSoundEffect;

        [SerializeField]
        private AudioClip _resumedSoundEffect;

        [SerializeField]
        private AudioClip _hiddenSoundEffect;

        [SerializeField]
        private AudioClip _closedSoundEffect;

        [SerializeField]
        private AudioClip _backgroundMusic;

        private IUiSoundSystem _uiSoundSystem;
        private UIScreen _uiScreen;

        [Inject]
        public void Initialize(IUiSoundSystem uiSoundSystem)
        {
            _uiSoundSystem = uiSoundSystem;
        }

        private void Awake()
        {
            _uiScreen = GetComponent<UIScreen>();
            _uiScreen.ScreenShownEvent += OnScreenScreenShown;
            _uiScreen.ScreenResumedEvent += OnScreenScreenResumed;
            _uiScreen.ScreenHiddenEvent += OnScreenScreenHidden;
            _uiScreen.ScreenClosedEvent += OnScreenScreenClosed;
        }

        private void OnDestroy()
        {
            _uiScreen.ScreenShownEvent -= OnScreenScreenShown;
            _uiScreen.ScreenResumedEvent -= OnScreenScreenResumed;
            _uiScreen.ScreenHiddenEvent -= OnScreenScreenHidden;
            _uiScreen.ScreenClosedEvent -= OnScreenScreenClosed;
        }

        private void OnEnable()
        {
            _uiSoundSystem.PlayBackgroundMusic(_backgroundMusic);
        }

        private void OnScreenScreenShown(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_shownSoundEffect);
        }

        private void OnScreenScreenResumed(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_resumedSoundEffect);
        }

        private void OnScreenScreenHidden(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_hiddenSoundEffect);
        }

        private void OnScreenScreenClosed(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_closedSoundEffect);
        }
    }
}