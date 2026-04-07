using System;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.Interfaces;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.Interactive.FunnelSelection
{
    public class PlayFunnelButton: MonoBehaviour
    {
        private UIButton _button;
        
        // Injected
        private IUiFunnelPresenter _funnelPresenter;
        private ILevelsDataProvider _levelsDataProvider;
        private IUILevelController _uiLevelController;

        [Inject]
        public void Initialize(
            IUiFunnelPresenter funnelPresenter,
            ILevelsDataProvider levelsDataProvider,
            IUILevelController uiLevelController)
        {
            _funnelPresenter = funnelPresenter;
            _levelsDataProvider = levelsDataProvider;
            _uiLevelController = uiLevelController;
        }

        private void Awake()
        {
            _button = GetComponent<UIButton>();
            _funnelPresenter.FunnelLoadedEvent += OnFunnelLoaded;
            _funnelPresenter.FunnelUnlockedEvent += OnFunnelUnlocked;
        }

        private void OnDestroy()
        {
            _funnelPresenter.FunnelLoadedEvent -= OnFunnelLoaded;
            _funnelPresenter.FunnelUnlockedEvent -= OnFunnelUnlocked;
        }

        private void OnEnable()
        {
            SetData();
        }
        
        private void OnFunnelLoaded(object sender, EventArgs e)
        {
            SetData();
        }
        private void OnFunnelUnlocked(object sender, int funnelId)
        {
            SetData();
        }
        
        private void SetData()
        {
            _button.SetData(new UIButtonViewData(
                label: new TextViewData(true, $"Play Level {_levelsDataProvider.GetLastUnlockedLevel().Id + 1}"),
                clickAction:OnButtonClicked,
                isVisible: !_funnelPresenter.IsLockedFunnel()));
        }

        private void OnButtonClicked()
        {
            _uiLevelController.StartLastUnlockedLevel();
        }
    }
}