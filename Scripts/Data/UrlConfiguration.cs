using JetBrains.Annotations;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Data
{
    [CreateAssetMenu(
        menuName = "Data/URLs Configuration",
        fileName = "UrlsConfigurationProvider")]
    public class UrlConfiguration : ScriptableObject, IUrlConfigurationProvider
    {
        [field: SerializeField]
        public string PrivacySettingsUrl { get; [UsedImplicitly]private set; }

        [field: SerializeField]
        public string PrivacyPolicyUrl { get; [UsedImplicitly]private set; }

        [field: SerializeField]
        public string TermsOfUseUrl { get; [UsedImplicitly]private set; }
        
        [field: SerializeField]
        public string RateUsUrl { get; [UsedImplicitly]private set; }
    }
}