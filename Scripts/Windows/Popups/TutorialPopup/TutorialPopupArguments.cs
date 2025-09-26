using System;
using UnityEngine;
using UnityEngine.Video;

namespace TwoOneTwoGames.UIManager.Windows.TutorialPopup
{
    public struct TutorialPopupArguments
    {
        public string Title { get; }

        public string Message { get; }

        public VideoClip TutorialClip { get; }
        public string ButtonText { get; }
        public Color ButtonTextColor { get; }
        public Action PopupResultAction { get; }
        public TutorialPopupArguments(
            string title, 
            string message, 
            string buttonText, 
            VideoClip tutorialClip,
            Color buttonTextColor,
            Action popupResultAction)
        {
            Title = title;
            Message = message;
            TutorialClip = tutorialClip;
            ButtonTextColor = buttonTextColor;
            ButtonText = buttonText;
            PopupResultAction = popupResultAction;
        }
    }
}