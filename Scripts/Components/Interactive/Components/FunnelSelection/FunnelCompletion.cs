using System;
using TMPro;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using TwoOneTwoGames.UIManager.Interfaces;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.Interactive.FunnelSelection
{
    public class FunnelCompletion: MonoBehaviour
    {
        private TextMeshProUGUI _text;
        
        // Injected
        private IUiFunnelPresenter _funnelPresenter;

        [Inject]
        public void Initialize(
            IUiFunnelPresenter funnelPresenter)
        {
            _funnelPresenter = funnelPresenter;
        }

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
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
            _text.SetData($"{_funnelPresenter.GetCompletedLevelsCount()} / {_funnelPresenter.GetLevelsCount()}");
        }
    }
}