using System.Globalization;
using TMPro;
using TwoOneTwoGames.UIManager.Data.IconPalette;
using TwoOneTwoGames.UIManager.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.NonInteractive.GameEconomy
{
    public class ResourceRewardView: MonoBehaviour
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
            _gameEconomyPresenter.EarnedResourceEvent += OnEarnedReward;
        }
        
        private void OnDestroy()
        {
            _gameEconomyPresenter.EarnedResourceEvent -= OnEarnedReward;
        }

        private void OnEarnedReward(int resourceId, long resourceValue)
        {
            if (_resourceId != resourceId)
            {
                return;
            }
            gameObject.SetActive(true);
            SetValue(resourceValue);
            SetIcon(resourceId);
        }

        private void SetValue(float value)
        {
            if (_resourceValueText == null)
            {
                return;
            }
            _resourceValueText.text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void SetIcon(int rewardId)
        {
            if (_resourceIcon == null)
            {
                return;
            }
            _resourceIcon.sprite = _uiGameEconomyIconPalette.EconomyResourceIcons[rewardId];
        }
    }
}