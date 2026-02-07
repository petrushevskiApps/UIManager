using System;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.Utilities.Extensions;
using TwoOneTwoGames.UIManager.Windows;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.Interactive.LevelsList
{
    public class LevelsList: MonoBehaviour, IListDataSource
    {
        [SerializeField]
        private InfiniteScrollController _infiniteScrollController;

        [SerializeField]
        private int _columnCount = 1;
        
        // Injected
        private ILevelsListViewModel _viewModel;
        private IItemViewPool _itemViewPool;
        private IUiHapticsController _uiHapticsController;
        private IUiFunnelPresenter _funnelPresenter;

        [Inject]
        private void Initialize(
            IUiFunnelPresenter funnelPresenter,
            ILevelsListViewModel viewModel,
            IItemViewPool itemViewPool,
            IUiHapticsController uiHapticsController)
        {
            _funnelPresenter = funnelPresenter;
            _viewModel = viewModel;
            _uiHapticsController = uiHapticsController;
            _itemViewPool = itemViewPool;
        }

        private void OnEnable()
        {
            _funnelPresenter.FunnelLoadedEvent += OnFunnelLoaded;
            _funnelPresenter.FunnelUnlockedEvent += OnFunnelUnlocked;
            _infiniteScrollController.ListEndEvent += OnListEndEvent;
            _infiniteScrollController.Setup(this, _itemViewPool);
            _viewModel.PageLoadedEvent += OnLevelsPageReady;
            _viewModel.Setup();
            _viewModel.LoadNextPage();
        }

        private void OnDisable()
        {
            _funnelPresenter.FunnelLoadedEvent -= OnFunnelLoaded;
            _funnelPresenter.FunnelUnlockedEvent -= OnFunnelUnlocked;
            _infiniteScrollController.ListEndEvent -= OnListEndEvent;
            _viewModel.PageLoadedEvent -= OnLevelsPageReady;

            _infiniteScrollController.Clear();
        }

        private void OnFunnelLoaded(object sender, EventArgs e)
        {
            ResetList();
        }

        private void OnFunnelUnlocked(object sender, int e)
        {
            ResetList();
        }

        private void ResetList()
        {
            _infiniteScrollController.Clear();
            _viewModel.Setup();
            _viewModel.LoadNextPage();
        }

        public void SetItemViewData(IItemView itemView)
        {
            var row = itemView.View.GetComponent<ListRowView>();

            row.SetData(
                _uiHapticsController,
                _viewModel.OnLevelClicked,
                !_funnelPresenter.IsLockedFunnel(),
                _viewModel.Levels.SafeGetRange(itemView.Index * _columnCount, _columnCount).ToArray());
        }
        
        private void OnListEndEvent(object sender, EventArgs e)
        {
            _viewModel.LoadNextPage();
        }

        private void OnLevelsPageReady(object sender, PageLoadedEventArguments args)
        {
            _infiniteScrollController.AddPageAndScrollTo(
                Mathf.CeilToInt((float) args.ElementsInPage / _columnCount),
                (Mathf.Clamp(args.ScrollToElementIndex - 1, 0, int.MaxValue)) / _columnCount);
        }
    }
}