using System.Diagnostics.CodeAnalysis;
using CookieConsent.Service;

namespace CookieConsent.Aspnet
{
    public sealed class AspnetCookieConsent
    {
        private static readonly object Sync = new object();
        private static AspnetCookieConsent _instance;
        
        [SuppressMessage("ReSharper", "InconsistentNaming")] 
        private ConsentService ConsentService;

        public static ConsentSettings Settings = new ConsentSettings();

        private AspnetCookieConsent() { }

        public static AspnetCookieConsent Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (Sync)
                    {
                        _instance = new AspnetCookieConsent
                        {
                            ConsentService = new ConsentService(
                                new HttpContextCookieStorage(), 
                                new FileAssetsProvider(Settings.LocalizedHtmlTemplateLocations, Settings.JsFileLocation, Settings.FallbackCulture, new FileShim())
                            )
                        };
                    }
                }
                return _instance;
            }
        }

        public static void SetDefaults(IAssetsProvider assetsProvider, ICookieStorage cookieStorage)
        {
            lock (Sync)
            {
                _instance = new AspnetCookieConsent {ConsentService = new ConsentService(cookieStorage, assetsProvider)};
            }
        }

        public static void SetDefaults(IAssetsProvider assetsProvider)
        {
            SetDefaults(assetsProvider, new HttpContextCookieStorage());
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