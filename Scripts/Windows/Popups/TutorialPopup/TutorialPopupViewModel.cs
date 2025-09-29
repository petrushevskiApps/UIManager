using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data.ExtensionsData;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using UnityEngine;
using UnityEngine.Video;

namespace TwoOneTwoGames.UIManager.Windows.TutorialPopup
{
    public class TutorialPopupViewModel
        : ITutorialPopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; }
        public IReactiveProperty<string> Message { get; }
        public IReactiveProperty<VideoPlayerViewData> VideoTutorial { get; }
        public IReactiveProperty<UIButtonViewData> ButtonViewData { get; }

        // Internal
        private Action _popupResultAction;

        // Injected
        private readonly INavigationController _navigationController;

        public TutorialPopupViewModel(INavigationController navigationController)
        {
            _navigationController = navigationController;
            Title = new ReactiveProperty<string>();
            Message = new ReactiveProperty<string>();
            VideoTutorial = new ReactiveProperty<VideoPlayerViewData>();
            ButtonViewData = new ReactiveProperty<UIButtonViewData>();
        }

        public void Setup(string title,
            string message,
            string buttonText,
            Color buttonTextColor,
            VideoClip videoClip, 
            Action popupResultAction)
        {
            _popupResultAction = popupResultAction;
            Title.Value = title;
            Message.Value = message;
            VideoTutorial.Value = new VideoPlayerViewData()
            {
                VideoClip = videoClip,
                IsLooping = true,
                IsAutoPlay = true
            };
            
            ButtonViewData.Value = new UIButtonViewData(
                label: new TextViewData(true, buttonText, buttonTextColor),
                isInteractive: true,
                clickAction: OkButtonClicked);
        }

        private void OkButtonClicked()
        {
            _navigationController.GoBack();
            _popupResultAction?.Invoke();
        }
        
        public void BackgroundClicked()
        {
            _navigationController.GoBack();
            _popupResultAction?.Invoke();
        }

        public void CloseClicked()
        {
            _navigationController.GoBack();
            _popupResultAction?.Invoke();
        }
    }
}