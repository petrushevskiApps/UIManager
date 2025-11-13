using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using TwoOneTwoGames.UIManager.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows.TutorialPopup
{
    public class TutorialPopup : UIPopup
    {
        [SerializeField]
        private VideoPlayer _videoPlayer;

        [SerializeField]
        private UIButton _button;

        // Injected
        private ITutorialPopupViewModel _viewModel;

        public override string ScreenTitle => gameObject.name + "_" + _viewModel.Title.Value;

        protected override IPopupViewModel GetPopupViewModel()
        {
            return _viewModel;
        }

        [Inject]
        private void Initialize(ITutorialPopupViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Show<TArguments>(TArguments navArguments)
        {
            if (navArguments is TutorialPopupArguments args)
            {
                _viewModel.Setup(
                    args.Title,
                    args.Message,
                    args.ButtonText, 
                    args.ButtonTextColor,
                    args.TutorialClip,
                    args.PopupResultAction);
            }

            base.Show(navArguments);
        }
        
        public override void Resume()
        {
            base.Resume();
            _viewModel.PopupResumed();
            _viewModel.VideoTutorial.Subscribe(_videoPlayer.SetData);
            _viewModel.ButtonViewData.Subscribe(_button.SetData);
        }

        public override void Hide()
        {
            base.Hide();

            _viewModel.VideoTutorial.Unsubscribe(_videoPlayer.SetData);
            _viewModel.ButtonViewData.Unsubscribe(_button.SetData);
        }
    }
}