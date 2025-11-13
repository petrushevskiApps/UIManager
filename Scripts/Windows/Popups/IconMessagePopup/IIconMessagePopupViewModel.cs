using System;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using TwoOneTwoGames.UIManager.Windows;
using UnityEngine;

namespace TwoOneTwoGames.ZenRings.UserInterface.Windows
{
    public interface IIconMessagePopupViewModel : IPopupViewModel
    {
        void Setup(
            string title, 
            string message, 
            Sprite icon, 
            Action discardAction,
            UIButtonViewData[] buttonsViewData);
        IReactiveProperty<ImageViewData> Icon { get; }
        List<IReactiveProperty<UIButtonViewData>> ButtonViews { get; }
        void Clear();
        void PopupResumed();
    }
}