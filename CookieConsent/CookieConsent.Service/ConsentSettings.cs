using System.Collections.Generic;

namespace CookieConsent.Service
{
    public class ConsentSettings
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LearnMoreLink { get; set; }
        public string LearnMoreLinkText { get; set; }
        public string CloseButtonTitle { get; set; }

        private Dictionary<string, string> _templateMappings;

        public Dictionary<string, string> GetMappings()
        {
            if (_templateMappings == null) FillDictionary();

            return _templateMappings;
        }

        private void FillDictionary()
        {
            _templateMappings = new Dictionary<string, string>()
            {
                { "title", Title },
                { "desc", Description },
                { "moreurl", LearnMoreLink },
                { "moretext", LearnMoreLinkText },
                { "close", CloseButtonTitle },
            };
        }
    }
}