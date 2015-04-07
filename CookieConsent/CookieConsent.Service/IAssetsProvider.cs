namespace CookieConsent.Service
{
    public interface IAssetsProvider
    {
        string HtmlElement { get; }
        string Javascript { get; }
    }
}