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
    /// played for every popup, handled in the UI Popup.
    /// </summary>
    [RequireComponent(typeof(UIPopup))]
    public class PopupCustomSoundAdder : MonoBehaviour
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
        private UIPopup _uiPopup;
    
        [Inject]
        public void Initialize(IUiSoundSystem uiSoundSystem)
        {
            _uiPopup = GetComponent<UIPopup>();
            _uiSoundSystem = uiSoundSystem;
            
            _uiPopup.PopupScreenShownEvent += OnPopupScreenShown;
            _uiPopup.PopupScreenResumedEvent += OnPopupScreenResumed;
            _uiPopup.PopupScreenHiddenEvent += OnPopupScreenHidden;
            _uiPopup.PopupScreenClosedEvent += OnPopupScreenClosed;
        }
        
        private void OnDestroy()
        {
            _uiPopup.PopupScreenShownEvent -= OnPopupScreenShown;
            _uiPopup.PopupScreenResumedEvent -= OnPopupScreenResumed;
            _uiPopup.PopupScreenHiddenEvent -= OnPopupScreenHidden;
            _uiPopup.PopupScreenClosedEvent -= OnPopupScreenClosed;
        }
    
        private void OnEnable()
        {
            _uiSoundSystem.PlayBackgroundMusic(_backgroundMusic);
        }
    
        private void OnPopupScreenShown(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_shownSoundEffect);
        }
    
        private void OnPopupScreenResumed(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_resumedSoundEffect);
        }
    
        private void OnPopupScreenHidden(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_hiddenSoundEffect);
        }
    
        private void OnPopupScreenClosed(object sender, EventArgs e)
        {
            _uiSoundSystem.PlayUiSoundEffect(_closedSoundEffect);
        }
    }
}