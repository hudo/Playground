using CookieConsent.Service;

namespace CookieConsent.Aspnet
{
    public sealed class AspnetConsent
    {
        private static object _sync = new object();
        private static AspnetConsent _aspnetConsent;
        private ConsentService _consentService;

        private AspnetConsent() { }

        public static AspnetConsent Default
        {
            get
            {
                if (_aspnetConsent == null)
                {
                    lock (_sync)
                    {
                        _aspnetConsent = new AspnetConsent();
                        _aspnetConsent._consentService = new ConsentService(new HttpContextCookieStorage(),new AssetsResource(), Settings); 
                    }
                }
                return _aspnetConsent;
            }
        }

        public string RenderConsent()
        {
            return _consentService.RenderNotificationHtml();
        }

        public static ConsentSettings Settings = new ConsentSettings();
    }
}