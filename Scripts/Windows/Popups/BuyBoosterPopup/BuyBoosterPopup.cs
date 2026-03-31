using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using TwoOneTwoGames.UIManager.Data.IconPalette;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows.Popups
{
    public class BuyBoosterPopup : UIPopup
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private UIButton _buyEchoesButton;

        [SerializeField]
        private UIButton _rewardedAdButton;
        
        public override string ScreenTitle => gameObject.name + "_" + _viewModel.Title.Value;
        
        // Internal

        // Injected
        private IBuyBoosterPopupViewModel _viewModel;


        protected override IPopupViewModel GetPopupViewModel()
        {
            return _viewModel;
        }

        [Inject]
        private void Initialize(IBuyBoosterPopupViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Show<TArguments>(TArguments navArguments)
        {
            if (navArguments is BuyBoosterPopupArguments args)
            {
                _viewModel.Setup(
                    args.Title,
                    args.Message,
                    args.Icon,
                    args.DiscardAction,
                    args.BoosterResourceId,
                    args.ResourceId,
                    args.BoosterCost,
                    args.ONSuccessfulBuy);
            }

            base.Show(navArguments);
        }
        
        public override void Resume()
        {
            base.Resume();

            _buyEchoesButton.OnClick += Close;
            _rewardedAdButton.OnClick += Close;
            
            _viewModel.Icon.Subscribe(_icon.SetData);
            _viewModel.BuyButtonViewData.Subscribe(_buyEchoesButton.SetData);
            _viewModel.RewardedAdButtonViewData.Subscribe(_rewardedAdButton.SetData);
        }

        public override void Hide()
        {
            base.Hide();

            _buyEchoesButton.OnClick -= Close;
            _rewardedAdButton.OnClick -= Close;

            _viewModel.Icon.Unsubscribe(_icon.SetData);
            _viewModel.BuyButtonViewData.Unsubscribe(_buyEchoesButton.SetData);
            _viewModel.RewardedAdButtonViewData.Unsubscribe(_rewardedAdButton.SetData);
        }
    }
}