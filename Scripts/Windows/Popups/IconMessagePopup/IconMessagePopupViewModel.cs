using System;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using UnityEngine;

namespace TwoOneTwoGames.ZenRings.UserInterface.Windows
{
    public class IconMessagePopupViewModel: IIconMessagePopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; }
        public IReactiveProperty<string> Message { get; }
        public IReactiveProperty<ImageViewData> Icon { get; }
        public List<IReactiveProperty<UIButtonViewData>> ButtonViews { get; }

        // Internal
        private Action _discardAction;
        private float _popupShownTime;

        // Injected
        private readonly INavigationController _navigationController;
        private readonly IUiAnalyticsEventsHandler _analyticsEventsHandler;

        public IconMessagePopupViewModel(
            INavigationController navigationController,
            IUiAnalyticsEventsHandler analyticsEventsHandler)
        {
            _navigationController = navigationController;
            _analyticsEventsHandler = analyticsEventsHandler;

            Title = new ReactiveProperty<string>();
            Message = new ReactiveProperty<string>();
            Icon = new ReactiveProperty<ImageViewData>();
            ButtonViews = new List<IReactiveProperty<UIButtonViewData>>();
        }

        public void Setup(
            string title, 
            string message, 
            Sprite icon, 
            Action discardAction,
            UIButtonViewData[] buttonsViewData)
        {
            Title.Value = title;
            Message.Value = message;
            Icon.Value = new ImageViewData(Color.white, true, icon);
            _discardAction = discardAction;
            foreach (var viewData in buttonsViewData)
            {
                ButtonViews.Add(new ReactiveProperty<UIButtonViewData>(viewData));
            }
        }

        public void PopupResumed()
        {
            _popupShownTime = Time.realtimeSinceStartup;
        }
        
        public void BackgroundClicked()
        {
            _analyticsEventsHandler.SendUiEvent(
                UiAnalyticsKeys.PlayerActions.Click, 
                "BackgroundButton", 
                GetPopupActiveTime());
            _discardAction?.Invoke();
            _navigationController.GoBack();
        }

        public void CloseClicked()
        {
            _analyticsEventsHandler.SendUiEvent(
                UiAnalyticsKeys.PlayerActions.Click, 
                "CloseButton", 
                GetPopupActiveTime());
            _discardAction?.Invoke();
            _navigationController.GoBack();
        }

        public void Clear()
        {
            ButtonViews.Clear();
            _discardAction = null;
        }
        
        private float GetPopupActiveTime()
        {
            return Time.realtimeSinceStartup - _popupShownTime;
        }
    }
}