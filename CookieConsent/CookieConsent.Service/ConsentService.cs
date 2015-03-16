namespace CookieConsent.Service
{
    public class ConsentService
    {
        private const string COOKIE_KEY = "CookieConsent";

        private readonly ICookieStorage _storage;
        private readonly IAssetsResource _assetsResource;
        private readonly ConsentSettings _settings;

        private string _cachedConsentHtml = "";

        public ConsentService(ICookieStorage storage, IAssetsResource assetsResource,ConsentSettings settings)
        {
            _storage = storage;
            _assetsResource = assetsResource;
            _settings = settings;
        }

        public string RenderNotificationHtml()
        {
            var cookieContent = _storage.Read(COOKIE_KEY);

            if (string.IsNullOrEmpty(cookieContent))
            {
                return GenerateConsentHtml();
            }
            return string.Empty;
        }

        private string GenerateConsentHtml()
        {
            if (string.IsNullOrEmpty(_cachedConsentHtml))
            {
                var template = _assetsResource.HtmlTemplate;
                foreach (var mapping in _settings.GetMappings())
                {
                    template = template.Replace(string.Format("{{{0}}}", mapping.Key), mapping.Value);
                }
                _cachedConsentHtml = template;
            }

            return _cachedConsentHtml;
        }
    }
}