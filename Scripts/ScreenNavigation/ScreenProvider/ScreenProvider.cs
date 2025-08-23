using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class ScreenProvider : MonoBehaviour, IScreenProvider
    {
        [SerializeField]
        [Tooltip("List of screens to be provided to Navigation Controller")]
        private List<UIScreen> _screens = new();

        public IScreen GetScreen<T>() where T : IScreen
        {
            return _screens.OfType<T>().FirstOrDefault();
        }
    }
}