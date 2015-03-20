namespace CookieConsent.Service
{
    public class ConsentService
    {
        private const string COOKIE_KEY = "CookieConsent";

        private readonly ICookieStorage _storage;
        private readonly IAssetsProvider _assetsProvider;

        private string _cachedConsentHtml = "";

        public ConsentService(ICookieStorage storage, IAssetsProvider assetsProvider)
        {
            _storage = storage;
            _assetsProvider = assetsProvider;
        }

        public string RenderNotificationHtml(ConsentSettings settings, string culture)
        {
            var cookieContent = _storage.Read(COOKIE_KEY);

            if (string.IsNullOrEmpty(cookieContent))
            {
                return GenerateConsentHtml(settings, culture);
            }
            return string.Empty;
        }

        private string GenerateConsentHtml(ConsentSettings settings, string culture)
        {
            if (string.IsNullOrEmpty(_cachedConsentHtml))
            {
                var template = _assetsProvider.GetHtml(culture);
                foreach (var mapping in settings.GetMappings())
                {
                    template = template.Replace(string.Format("{{{0}}}", mapping.Key), mapping.Value);
                }
                _cachedConsentHtml = template;
            }

            return _cachedConsentHtml;
        }
    }
}