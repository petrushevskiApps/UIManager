using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Windows.Popups
{
    public interface IBuyBoosterPopupViewModel : IPopupViewModel
    {
        IReactiveProperty<ImageViewData> Icon { get; }
        IReactiveProperty<UIButtonViewData> BuyButtonViewData { get; }
        IReactiveProperty<UIButtonViewData> RewardedAdButtonViewData { get; }

        void Setup(string title,
            string message,
            Sprite icon,
            Action discardAction,
            int boosterResourceId,
            int resourceId,
            int resourceAmountRequired, Action argsOnSuccessfullBuy);
    }
}