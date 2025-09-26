using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Video;

namespace TwoOneTwoGames.UIManager.Data.Tutorial
{
    [CreateAssetMenu(
        fileName = "TutorialPopupData-Level_",
        menuName = "Game Data/Tutorial Popup/Tutorial Popup Data",
        order = 1)]
    public class TutorialPopupData: ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; [UsedImplicitly] private set; }
        [field: SerializeField]
        public string Message { get; [UsedImplicitly] private set; }
        [field: SerializeField]
        public string ButtonText { get; [UsedImplicitly] private set; }
        [field: SerializeField]
        public Color ButtonTextColor { get; [UsedImplicitly] private set; }
        [field: SerializeField]
        public VideoClip TutorialClip { get; [UsedImplicitly] private set; }
    }
}