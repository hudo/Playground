namespace CookieConsent.Service
{
    public interface ICookieStorage
    {
        string Read(string key);
        void Store(string key, string content);
    }
}