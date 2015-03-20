namespace CookieConsent.Service
{
    public interface IAssetsProvider
    {
        string GetHtml(string culture);
        string Javascript { get; }
    }
}