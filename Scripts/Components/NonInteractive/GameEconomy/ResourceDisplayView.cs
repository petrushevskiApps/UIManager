using System;
using System.Globalization;
using TMPro;
using TwoOneTwoGames.UIManager.Data.IconPalette;
using TwoOneTwoGames.UIManager.Interfaces;
using TwoOneTwoGames.UIManager.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.NonInteractive.GameEconomy
{
    public class ResourceDisplayView: MonoBehaviour
    {
        [SerializeField]
        private int _resourceId;
        [SerializeField]
        private Image _resourceIcon;
        [SerializeField]
        private TextMeshProUGUI _resourceValueText;

        // Injected
        private IUiGameEconomyPresenter _gameEconomyPresenter;
        private IUiGameEconomyIconPalette _uiGameEconomyIconPalette;

        [Inject]
        public void Initialize(
            IUiGameEconomyIconPalette uiGameEconomyIconPalette,
            IUiGameEconomyPresenter gameEconomyPresenter)
        {
            _uiGameEconomyIconPalette = uiGameEconomyIconPalette;
            _gameEconomyPresenter = gameEconomyPresenter;
        }

        private void OnEnable()
        {
            _gameEconomyPresenter.UsedResourceEvent += OnResourceUsed;
            SetResource();
        }

        private void OnDisable()
        {
            _gameEconomyPresenter.UsedResourceEvent -= OnResourceUsed;
        }

        private void OnResourceUsed(object sender, (int id, float value) resource)
        {
            if (_resourceId == resource.id)
            {
                SetResource();
            }
        }

        private void SetResource()
        {
            SetValue(_gameEconomyPresenter.GetResourceValueWithId(_resourceId));
            SetIcon(_resourceId);
        }

        private void SetValue(float value)
        {
            _resourceValueText.text = NumberFormatter.FormatWithSuffix(value, true, SuffixThreshold.Million);
        }

        private void SetIcon(int rewardId)
        {
            _resourceIcon.sprite = _uiGameEconomyIconPalette.EconomyResourceIcons[rewardId];
        }
    }
}