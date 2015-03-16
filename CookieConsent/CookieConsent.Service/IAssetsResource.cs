namespace CookieConsent.Service
{
    public interface IAssetsResource
    {
        string HtmlTemplate { get; }
        string Javascript { get; }
    }
}