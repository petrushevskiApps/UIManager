using System;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
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

        // Injected
        private readonly INavigationController _navigationController;

        public IconMessagePopupViewModel(
            INavigationController navigationController)
        {
            _navigationController = navigationController;

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

        public void BackgroundClicked()
        {
            _discardAction?.Invoke();
            _navigationController.GoBack();
        }

        public void CloseClicked()
        {
            _discardAction?.Invoke();
            _navigationController.GoBack();
        }

        public void Clear()
        {
            ButtonViews.Clear();
            _discardAction = null;
        }
    }
}