using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class PopupScreenProvider : MonoBehaviour, IScreenProvider
    {
        [SerializeField]
        [Tooltip("List of screens to be provided to Navigation Controller")]
        private List<UIPopup> _popups = new();

        [Inject]
        private INavigationController _navigationController;

        [Inject]
        private IPopupNavigation _popupNavigation;

        private void Awake()
        {
            _navigationController.AllScreensClosedEvent += OnAllScreensClosed;
        }

        private void OnDestroy()
        {
            _navigationController.AllScreensClosedEvent -= OnAllScreensClosed;
        }

        public IScreen GetScreen<T>() where T : IScreen
        {
            return _popups.Find(x => x != null && x.GetType() == typeof(T));
        }

        private void OnAllScreensClosed(object sender, EventArgs e)
        {
            _popupNavigation.ShowExitGamePopup();
        }
    }
}