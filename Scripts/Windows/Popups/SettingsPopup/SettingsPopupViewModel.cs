using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class SettingsPopupViewModel : ISettingsPopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; protected set; }
        public IReactiveProperty<string> Message { get; protected set; }
        public IReadOnlyReactiveProperty<ToggleViewData> AudioToggle => _audioToggle;
        public IReadOnlyReactiveProperty<ToggleViewData> MusicToggle => _musicToggle;
        public IReadOnlyReactiveProperty<ToggleViewData> VibrationToggle => _vibrationToggle;
        public IReactiveProperty<UIButtonViewData> PrivacyPolicyButton { get; }
        public IReactiveProperty<UIButtonViewData> PrivacySettingsButton { get; }
        public IReactiveProperty<UIButtonViewData> TermsOfUseButton { get; }
        public IReactiveProperty<UIButtonViewData> RateUsButton { get; }

        // Internal
        private readonly IReactiveProperty<ToggleViewData> _audioToggle;
        private readonly IReactiveProperty<ToggleViewData> _musicToggle;
        private readonly INavigationController _navigationController;
        private readonly ISettingsStateProvider _settingsStateProvider;
        private readonly ISettingsStateUpdater _settingsStateUpdater;
        private readonly IUiAnalyticsEventsHandler _analyticsEventsHandler;

        // Injected
        private readonly IUrlConfigurationProvider _urlConfigurationProvider;
        private readonly IReactiveProperty<ToggleViewData> _vibrationToggle;


        public SettingsPopupViewModel(
            IUrlConfigurationProvider urlConfigurationProvider,
            INavigationController navigationController,
            ISettingsStateProvider settingsStateProvider,
            ISettingsStateUpdater settingsStateUpdater,
            IUiAnalyticsEventsHandler analyticsEventsHandler)
        {
            _urlConfigurationProvider = urlConfigurationProvider;
            _navigationController = navigationController;
            _settingsStateProvider = settingsStateProvider;
            _settingsStateUpdater = settingsStateUpdater;
            _analyticsEventsHandler = analyticsEventsHandler;

            _audioToggle = new ReactiveProperty<ToggleViewData>();
            _musicToggle = new ReactiveProperty<ToggleViewData>();
            _vibrationToggle = new ReactiveProperty<ToggleViewData>();
            SetToggles();

            Title = new ReactiveProperty<string>("Settings");
            Message = new ReactiveProperty<string>("Thank you for playing our game.");

            PrivacyPolicyButton = new ReactiveProperty<UIButtonViewData>(
                new UIButtonViewData(
                    label: new TextViewData(true, "Privacy Policy"),
                    clickAction: PrivacyPolicyClicked));
            PrivacySettingsButton = new ReactiveProperty<UIButtonViewData>(
                new UIButtonViewData(
                    label: new TextViewData(true, "Privacy Settings"),
                    isVisible: false,
                    clickAction: PrivacySettingsClicked));
            TermsOfUseButton = new ReactiveProperty<UIButtonViewData>(
                new UIButtonViewData(
                    label: new TextViewData(true,  "Terms of Use"),
                    clickAction: TermsOfUseClicked));
            RateUsButton = new ReactiveProperty<UIButtonViewData>(
                new UIButtonViewData(
                    label: new TextViewData(true,  "Rate Us"),
                    clickAction: RateUsClicked));
        }

        protected virtual void RateUsClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "Popup:Settings:RateUsButton");
            OpenURL(_urlConfigurationProvider.RateUsUrl);
        }

        protected virtual void TermsOfUseClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "Popup:Settings:TermsOfUseButton");
            OpenURL(_urlConfigurationProvider.TermsOfUseUrl);
        }

        protected virtual void PrivacyPolicyClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "Popup:Settings:PrivacyPolicyButton");
            OpenURL(_urlConfigurationProvider.PrivacyPolicyUrl);
        }

        protected virtual void PrivacySettingsClicked()
        {
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Click, "Popup:Settings:PrivacySettingsButton");
            OpenURL(_urlConfigurationProvider.PrivacySettingsUrl);
        }

        public void BackgroundClicked()
        {
            _navigationController.GoBack();
        }

        public void CloseClicked()
        {
            _navigationController.GoBack();
        }

        private void SetToggles()
        {
            _audioToggle.Value = new ToggleViewData
            {
                Label = "Sound Effects",
                State = _settingsStateProvider.IsSoundEffectsActive,
                OnToggleStateChanged = AudioToggleClicked,
                IsActive = true
            };
            _musicToggle.Value = new ToggleViewData
            {
                Label = "Game Music",
                State = _settingsStateProvider.IsMusicActive,
                OnToggleStateChanged = MusicToggleClicked,
                IsActive = true
            };
            _vibrationToggle.Value = new ToggleViewData
            {
                Label = "Vibrations",
                State = _settingsStateProvider.IsVibrationsActive,
                OnToggleStateChanged = VibrationToggleClicked,
                IsActive = true
            };
        }

        protected virtual void AudioToggleClicked()
        {
            var newState = !_audioToggle.Value.State;
            _audioToggle.Value = _audioToggle.Value with
            {
                State = newState
            };
            _settingsStateUpdater.UpdateSoundEffectsState(newState);
            string state = newState
                ? "ON"
                : "OFF";
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Toggle, $"soundEffects_{state}");
        }

        protected virtual void MusicToggleClicked()
        {
            var newState = !_musicToggle.Value.State;
            _musicToggle.Value = _musicToggle.Value with
            {
                State = newState
            };
            _settingsStateUpdater.UpdateGameMusicState(newState);
            string state = newState
                ? "ON"
                : "OFF";
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Toggle, $"music_{state}");
        }

        protected virtual void VibrationToggleClicked()
        {
            var newState = !_vibrationToggle.Value.State;
            _vibrationToggle.Value = _vibrationToggle.Value with
            {
                State = newState
            };
            _settingsStateUpdater.UpdateVibrationsState(newState);
            string state = newState
                ? "ON"
                : "OFF";
            _analyticsEventsHandler.SendUiEvent(UiAnalyticsKeys.PlayerActions.Toggle, $"vibrations_{state}");
        }

        private void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}