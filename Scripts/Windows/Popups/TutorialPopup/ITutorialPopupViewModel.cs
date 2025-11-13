using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Data.ExtensionsData;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using UnityEngine;
using UnityEngine.Video;

namespace TwoOneTwoGames.UIManager.Windows.TutorialPopup
{
    public interface ITutorialPopupViewModel : IPopupViewModel
    {
        IReactiveProperty<VideoPlayerViewData> VideoTutorial { get; }
        IReactiveProperty<UIButtonViewData> ButtonViewData { get; }

        void Setup(string title,
            string message,
            string buttonText,
            Color buttonTextColor,
            VideoClip videoClip, Action argsPopupResultAction);

        void PopupResumed();
    }
}