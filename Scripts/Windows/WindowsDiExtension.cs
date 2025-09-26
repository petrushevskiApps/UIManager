using TwoOneTwoGames.UIManager.Windows.Popups;
using TwoOneTwoGames.UIManager.Windows.TutorialPopup;
using TwoOneTwoGames.ZenRings.UserInterface.Windows;
using Zenject;

namespace TwoOneTwoGames.UIManager.Windows
{
    public static class WindowsDiExtension
    {
        public static void BindMainScreenDependencies(this DiContainer container)
        {
            container
                .Bind<IMainScreenViewModel>()
                .To<MainScreenViewModel>()
                .AsSingle();
        }
        public static void BindInGameScreenDependencies(this DiContainer container)
        {
            container
                .Bind<IInGameScreenViewModel>()
                .To<InGameScreenViewModel>()
                .AsSingle();
        }
        public static void BindLevelFailedScreenDependencies(this DiContainer container)
        {
            container
                .Bind<ILevelFailedScreenViewModel>()
                .To<LevelFailedScreenViewModel>()
                .AsSingle();
        }
        public static void BindLevelCompletedScreenDependencies(this DiContainer container)
        {
            container
                .Bind<ILevelCompletedScreenViewModel>()
                .To<LevelCompletedScreenViewModel>()
                .AsSingle();
        }
        public static void BindLevelsScreenDependencies(this DiContainer container)
        {
            container
                .Bind<ILevelsScreenViewModel>()
                .To<LevelsScreenViewModel>()
                .AsSingle();
        }
        public static void BindPopupDependencies(this DiContainer container)
        {
            container
                .Bind<INoInternetPopupViewModel>()
                .To<NoInternetPopupViewModel>()
                .AsSingle();
            container
                .Bind<IPausePopupViewModel>()
                .To<PausePopupViewModel>()
                .AsSingle();
            container
                .Bind<ISettingsPopupViewModel>()
                .To<SettingsPopupViewModel>()
                .AsSingle();
            container
                .Bind<IExitLevelPopupViewModel>()
                .To<ExitLevelPopupViewModel>()
                .AsSingle();
            container
                .Bind<IExitGamePopupViewModel>()
                .To<ExitGamePopupViewModel>()
                .AsSingle();
            container
                .BindIFactory<IIconMessagePopupViewModel>()
                .To<IconMessagePopupViewModel>()
                .AsSingle();
            container
                .Bind<IBuyBoosterPopupViewModel>()
                .To<BuyBoosterPopupViewModel>()
                .AsSingle();
            container
                .Bind<ITutorialPopupViewModel>()
                .To<TutorialPopupViewModel>()
                .AsSingle();
        }
    }
}