using System;
using System.Collections;
using TMPro;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.Windows;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public abstract class UIPopup : MonoBehaviour, IScreen, IPopupScreenEvents
    {
        [SerializeField]
        [Tooltip("True: Popup goes on backstack when hidden. False: One time popup.")]
        private bool _isBackStackable = true;

        [SerializeField]
        [Tooltip("True: When this popup is shown the Game Time is set to 0. False: Ignores this setting.")]
        private bool _pauseGameWhenActive;

        [SerializeField]
        [Tooltip("Clickable background which disposes the popup when clicked.Same as the Back Button.")]
        private UIButton _popupClickableBackground;

        [SerializeField]
        [Tooltip("Button which discards popups.")]
        private UIButton _closeButton;
        
        [SerializeField] 
        private PopupOpenCloseAnimation _openCloseAnimation;
        
        [Header("Popup Properties")]
        [SerializeField]
        private TextMeshProUGUI _title;
        [SerializeField]
        private TextMeshProUGUI _message;

        private IUiAudioPalette _uiAudioPalette;

        // Injected
        private IUiSoundSystem _uiSoundSystem;
        protected INavigationController NavigationController;
        private IPauseGameController _pauseGameController;

        // Events
        public event EventHandler PopupScreenShownEvent;
        public event EventHandler PopupScreenResumedEvent;
        public event EventHandler PopupScreenHiddenEvent;
        public event EventHandler PopupScreenClosedEvent;

        public bool IsPopup => true;
        public bool IsBackStackable => _isBackStackable;

        [Inject]
        public void Initialize(
            IUiSoundSystem uiSoundSystem,
            IUiAudioPalette uiAudioPalette,
            INavigationController navigationController, 
            IPauseGameController pauseGameController)
        {
            _uiSoundSystem = uiSoundSystem;
            _uiAudioPalette = uiAudioPalette;
            NavigationController = navigationController;
            _pauseGameController = pauseGameController;
        }

        public virtual void Show<TArguments>(TArguments navArguments)
        {
            PopupScreenShownEvent?.Invoke(this, EventArgs.Empty);
            Resume();
        }

        public virtual void Resume()
        {
            PopupScreenResumedEvent?.Invoke(this, EventArgs.Empty);
            _popupClickableBackground.OnClick += GetPopupViewModel().BackgroundClicked;
            if (_closeButton != null)
            {
                _closeButton.OnClick += GetPopupViewModel().CloseClicked;
            }
            if (_title != null)
            {
                GetPopupViewModel().Title?.Subscribe(SetTitle, true);
            }

            if (_message != null)
            {
                GetPopupViewModel().Message?.Subscribe(SetMessage, true);
            }
            gameObject.SetActive(true);
            PlaySfx(_uiAudioPalette.PopupShown);
            PauseGame(true);
            if (_openCloseAnimation != null)
            {
                _openCloseAnimation.OpenPopup();
            }
        }
        
        public virtual void Hide()
        {
            if (_openCloseAnimation != null)
            {
                HideInternal(false);
                _openCloseAnimation.ClosePopup(() =>
                {
                    gameObject.SetActive(false);
                });
            }
            else
            {
                HideInternal(true);
            }
        }

        private void HideInternal(bool hideObject)
        {
            PopupScreenHiddenEvent?.Invoke(this, EventArgs.Empty);
            _popupClickableBackground.OnClick -= GetPopupViewModel().BackgroundClicked;
            if (_closeButton != null)
            {
                _closeButton.OnClick -= GetPopupViewModel().CloseClicked;
            }
            if (hideObject)
            {
                gameObject.SetActive(false);
            }
            if (_title != null)
            {
                GetPopupViewModel().Title?.Unsubscribe(SetTitle);
            }

            if (_message != null)
            {
                GetPopupViewModel().Message?.Unsubscribe(SetMessage);
            }
            PauseGame(false);
        }
        public virtual void Close()
        {
            PopupScreenClosedEvent?.Invoke(this, EventArgs.Empty);
            PlaySfx(_uiAudioPalette.PopupHidden);
            Hide();
        }

        public void OnBackTriggered()
        {
            NavigationController.GoBack();
        }

        protected abstract IPopupViewModel GetPopupViewModel();

        private void PlaySfx(AudioClip sfxClip)
        {
            if (sfxClip != null) _uiSoundSystem?.PlayUiSoundEffect(sfxClip);
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                _title.gameObject.SetActive(false);
                return;
            }

            _title.gameObject.SetActive(true);
            _title.text = title;
        }

        private void SetMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                _message.gameObject.SetActive(false);
                return;
            }

            _message.gameObject.SetActive(true);
            _message.text = message;
        }

        private void PauseGame(bool pause)
        {
            if (_pauseGameWhenActive)
            {
                _pauseGameController.TogglePauseGame(pause);
            }
        }
    }
}