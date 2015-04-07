using System;
using System.Collections.Concurrent;

namespace CookieConsent.Service
{
    public class ConsentService
    {
        private const string COOKIE_KEY = "CookieConsent";

        private readonly ICookieStorage _storage;
        private readonly IAssetsProvider _assetsProvider;

        private static readonly ConcurrentDictionary<string, string> CachedConsentHtml = new ConcurrentDictionary<string, string>();

        public ConsentService(ICookieStorage storage, IAssetsProvider assetsProvider)
        {
            _storage = storage;
            _assetsProvider = assetsProvider;
        }

        public string RenderNotificationHtml(ConsentSettings settings, string culture)
        {
            var cookieContent = _storage.Read(COOKIE_KEY);

            return string.IsNullOrEmpty(cookieContent) ? GenerateConsentHtml(settings, culture) : string.Empty;
        }

        private string GenerateConsentHtml(ConsentSettings settings, string culture)
        {
            return CachedConsentHtml.GetOrAdd(culture, x =>
            {
                var template = _assetsProvider.HtmlElement;
                var mappings = settings.GetMappings(culture) ?? settings.GetMappings(settings.FallbackCulture);

                if(mappings == null) throw new Exception("Can't find culture and fallback culture");

                foreach (var mapping in mappings)
                    template = template.Replace(string.Format("{{{0}}}", mapping.Key), mapping.Value);
                
                return template;
            });
        }
    }
}