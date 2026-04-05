using System;
using TwoOneTwoGames.UIManager.Components.Interactive;
using TwoOneTwoGames.UIManager.Components.NonInteractive;
using TwoOneTwoGames.UIManager.Components.NonInteractive.NonInteractive.ViewData;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TwoOneTwoGames.UIManager.Windows
{
    public class LevelItemView : MonoBehaviour, IItemView
    {
        [SerializeField]
        private UIStars _stars;

        [SerializeField]
        private UIButton _button;

        [SerializeField]
        protected Image _background;

        [SerializeField]
        private GameObject _lockIcon;

        [Header("Backgrounds")]
        [SerializeField]
        private Sprite _lockedBackground;

        [SerializeField]
        private Sprite _unlockedBackground;

        [SerializeField]
        private Sprite _completedBackground;

        private IUILevelData _levelData;

        private Action<int, int> _onItemClicked;
        private bool _isFunnelUnlocked;

        public int Index { get; set; }
        public GameObject View => gameObject;

        public virtual void SetData(
            IUiHapticsController uiHapticsController,
            IUILevelData levelData,
            bool isFunnelUnlocked,
            Action<int, int> onItemClicked)
        {
            _isFunnelUnlocked = isFunnelUnlocked;
            _levelData = levelData;
            
            gameObject.name = $"Level-{_levelData.Id + 1}";
            _onItemClicked = onItemClicked;
            _button.SetHaptics(uiHapticsController);
            SetState();
        }

        private void SetState()
        {
            var visualLevelId = _levelData.Id + 1;
            if (_isFunnelUnlocked && _levelData.IsCompleted)
            {
                _background.sprite = _completedBackground;
                _lockIcon.SetActive(false);
                _button.SetData(new UIButtonViewData(
                    label: new TextViewData(true, $"{visualLevelId.ToString()}"),
                    clickAction:OnButtonClicked));
                _stars.SetData(_levelData.Stars);
            }
            else if (_isFunnelUnlocked && _levelData.IsUnlocked)
            {
                _background.sprite = _unlockedBackground;
                _lockIcon.SetActive(false);
                _button.SetData(new UIButtonViewData(
                    label: new TextViewData(true, $"{visualLevelId.ToString()}"),
                    clickAction:OnButtonClicked));
                _stars.SetData(_levelData.Stars);
            }
            else
            {
                _background.sprite = _lockedBackground;
                _lockIcon.SetActive(true);
                _button.SetData(new UIButtonViewData(
                    isInteractive: false,
                    clickAction:OnButtonClicked));
                _stars.gameObject.SetActive(false);
            }
        }

        private void OnButtonClicked()
        {
            _onItemClicked?.Invoke(_levelData.WorldId, _levelData.Id);
        }
    }
}