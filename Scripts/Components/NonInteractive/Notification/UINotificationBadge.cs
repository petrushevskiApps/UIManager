using TMPro;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Components.NonInteractive
{
    /// <summary>
    /// The "something is waiting for you here" dot that sits on an entry point, carrying how many
    /// things are waiting. It hides itself at zero, so a caller only ever hands it a count.
    /// </summary>
    public class UINotificationBadge : MonoBehaviour
    {
        // Past this the digits stop fitting the dot and stop meaning anything to the player.
        private const int MaxShownCount = 99;

        [SerializeField]
        private TextMeshProUGUI _countLabel;

        public void SetData(UINotificationBadgeData badgeData)
        {
            if (badgeData.Count <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
            if (_countLabel != null)
            {
                _countLabel.text = badgeData.Count > MaxShownCount
                    ? $"{MaxShownCount}+"
                    : badgeData.Count.ToString();
            }
        }
    }
}
