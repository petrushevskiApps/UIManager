namespace TwoOneTwoGames.UIManager.Data
{
    public interface IUrlConfigurationProvider
    {
        string PrivacySettingsUrl { get; }
        string PrivacyPolicyUrl { get; }
        string TermsOfUseUrl { get; }
        string RateUsUrl { get; }
    }
}