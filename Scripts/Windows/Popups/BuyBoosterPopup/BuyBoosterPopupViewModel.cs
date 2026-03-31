using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Data.IconPalette;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.ReactiveProperty;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Windows.Popups
{
    public class BuyBoosterPopupViewModel : IBuyBoosterPopupViewModel
    {
        // Reactive Properties
        public IReactiveProperty<string> Title { get; }
        public IReactiveProperty<string> Message { get; }
        public IReactiveProperty<ImageViewData> Icon { get; }
        public IReactiveProperty<UIButtonViewData> BuyButtonViewData { get; }
        public IReactiveProperty<UIButtonViewData> RewardedAdButtonViewData { get; }

        // Internal
        private Action _discardAction;
        private Action _onSuccessfulBuy;
        private int _resourceId;
        private int _resourceAmountRequired;

        // Injected
        private readonly IUiGameEconomyPresenter _economyPresenter;
        private readonly IUiGameEconomyController _economyController;
        private readonly INavigationController _navigationController;
        private readonly IUiGameEconomyIconPalette _gameEconomyIconPalette;
        private readonly IAdsIconPalette _adsIconPalette;
        private readonly IUiAnalyticsEventsHandler _analyticsEventsHandler;

        public BuyBoosterPopupViewModel(
            IUiGameEconomyPresenter economyPresenter,
            IUiGameEconomyController economyController,
            INavigationController navigationController,
            IUiGameEconomyIconPalette gameEconomyIconPalette,
            IAdsIconPalette adsIconPalette,
            IUiAnalyticsEventsHandler analyticsEventsHandler)
        {
            _economyPresenter = economyPresenter;
            _economyController = economyController;
            _navigationController = navigationController;
            _gameEconomyIconPalette = gameEconomyIconPalette;
            _adsIconPalette = adsIconPalette;
            _analyticsEventsHandler = analyticsEventsHandler;

            Title = new ReactiveProperty<string>();
            Message = new ReactiveProperty<string>();
            Icon = new ReactiveProperty<ImageViewData>();
            BuyButtonViewData = new ReactiveProperty<UIButtonViewData>();
            RewardedAdButtonViewData = new ReactiveProperty<UIButtonViewData>();
        }

        public void Setup(string title,
            string message,
            Sprite icon,
            Action discardAction,
            int boosterResourceId,
            int resourceId,
            int resourceAmountRequired,
            Action onSuccessfulBuy)
        {
            Title.Value = title;
            Message.Value = message;
            Icon.Value = new ImageViewData(Color.white, true, icon);
            _discardAction = discardAction;
            _onSuccessfulBuy = onSuccessfulBuy;
            _resourceId = resourceId;
            _resourceAmountRequired = resourceAmountRequired;

            bool isInteractiveBuyButton = IsBuyWithResourceAvailable(resourceId, resourceAmountRequired);
            BuyButtonViewData.Value = new UIButtonViewData(
                label: new TextViewData(true, $"{resourceAmountRequired}",
                    isInteractiveBuyButton ? Color.white : Color.yellow),
                secondIcon: new ImageViewData(
                    sprite: _gameEconomyIconPalette.EconomyResourceIcons[_resourceId],
                    color: Color.white,
                    isActive: true),
                isInteractive: isInteractiveBuyButton,
                clickAction: OnBuyButtonClicked);
            RewardedAdButtonViewData.Value = new UIButtonViewData(
                label: new TextViewData(true, "Free"),
                secondIcon: new ImageViewData(
                    sprite: _adsIconPalette.RewardedAdsIcon,
                    color: Color.white,
                    isActive: true),
                isInteractive: false,
                isVisible: false,
                clickAction: OnRewardedAdClicked);
        }

        private void OnRewardedAdClicked()
        {
            _analyticsEventsHandler.SendUiEvent(
                UiAnalyticsKeys.PlayerActions.Click, 
                "RewardedAdButton", 
                _navigationController.GetActiveScreenTime());
            _onSuccessfulBuy?.Invoke();
            _navigationController.GoBack();
        }

        private void OnBuyButtonClicked()
        {
            _analyticsEventsHandler.SendUiEvent(
                UiAnalyticsKeys.PlayerActions.Click, 
                "BuyButton", 
                _navigationController.GetActiveScreenTime());
            _economyController.UseCurrency(_resourceId, _resourceAmountRequired);
            _onSuccessfulBuy?.Invoke();
            _navigationController.GoBack();
        }

        private bool IsBuyWithResourceAvailable(int resourceId, int resourceAmount)
        {
            int availableResources = _economyPresenter.GetResourceValueWithId(resourceId);
            return availableResources >= resourceAmount;
        }

        public void BackgroundClicked()
        {
            Close();
        }

        public void CloseClicked()
        {
            Close();
        }

        private void Close()
        {
            _discardAction?.Invoke();
            _navigationController.GoBack();
        }
    }
}