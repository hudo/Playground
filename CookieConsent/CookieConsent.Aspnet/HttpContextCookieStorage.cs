using System.Web;
using CookieConsent.Service;

namespace CookieConsent.Aspnet
{
    public class HttpContextCookieStorage : ICookieStorage
    {
        public string Read(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            return cookie != null ? cookie.Value : string.Empty;
        }

        public void Store(string key, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}