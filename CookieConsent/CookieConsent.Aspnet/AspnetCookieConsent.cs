using System.Diagnostics.CodeAnalysis;
using System.Web;
using CookieConsent.Service;

namespace CookieConsent.Aspnet
{
    public sealed class AspnetCookieConsent
    {
        private static readonly object Sync = new object();
        private static AspnetCookieConsent _instance;
        
        [SuppressMessage("ReSharper", "InconsistentNaming")] 
        private ConsentService ConsentService;

        public static ConsentSettings Settings { get; set; }

        static AspnetCookieConsent()
        {
            Settings = new ConsentSettings();
        }

        private AspnetCookieConsent() { }

        public static AspnetCookieConsent Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (Sync)
                    {
                        var wwwroot = HttpContext.Current.Server.MapPath("\\");
                        _instance = new AspnetCookieConsent
                        {
                            ConsentService = new ConsentService(
                                new HttpContextCookieStorage(), 
                                new CachedAssetsProvider(new FileShim(wwwroot), Settings))
                        };
                    }
                }
                return _instance;
            }
        }

        public static void SetDefaults(IAssetsProvider assetsProvider = null, ICookieStorage cookieStorage = null)
        {
            lock (Sync)
            {
                var wwwroot = HttpContext.Current.Server.MapPath("\\");
                _instance = new AspnetCookieConsent
                {
                    ConsentService = new ConsentService(
                        cookieStorage ?? new HttpContextCookieStorage(), 
                        assetsProvider ?? new CachedAssetsProvider(new FileShim(wwwroot), Settings))
                };
            }
        }
      
        public string RenderConsent()
        {
            return ConsentService.RenderNotificationHtml(Settings, "default");
        }

        public string RenderConsent(string culture)
        {
            return ConsentService.RenderNotificationHtml(Settings, culture);
        }
        
    }
}