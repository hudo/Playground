using CookieConsent.Service;

namespace CookieConsent.Aspnet
{
    public class HttpContextCookieStorage : ICookieStorage
    {
        public string Read(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Store(string key, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}