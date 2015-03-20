namespace CookieConsent.Service
{
    public interface IFileShim
    {
        bool Exists(string location);
        string ReadAllText(string location);
    }
}