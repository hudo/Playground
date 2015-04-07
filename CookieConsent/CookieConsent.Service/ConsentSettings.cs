using System.Collections.Generic;

namespace CookieConsent.Service
{
    public class ConsentSettings
    {
        public ConsentSettings()
        {
            LocalizedContentSettings = new Dictionary<string, LocalizedContent>()
            {
                {"default", new LocalizedContent()
                {
                    Title = "Cookie consent",
                    Description = "description",
                    LearnMoreLink = "http://www.google.com",
                    LearnMoreLinkText = "Learn more",
                    CloseButtonTitle = "Accept"
                }}
            };
        }

        public string JsFileLocation = "/Assets/CookieConsent.js";
        public string HtmlFileLocation = "/Assets/CookieConsent.html";

        public Dictionary<string, LocalizedContent> LocalizedContentSettings;
        public string FallbackCulture;

        public Dictionary<string, string> GetMappings(string culture)
        {
            if (!LocalizedContentSettings.ContainsKey(culture))
                return null;

            var content = LocalizedContentSettings[culture];

            return new Dictionary<string, string>()
            {
                { "title", content.Title },
                { "desc", content.Description },
                { "moreurl", content.LearnMoreLink },
                { "moretext", content.LearnMoreLinkText },
                { "close", content.CloseButtonTitle },
            };
        }

        public class LocalizedContent
        {
            public string Title;
            public string Description;
            public string LearnMoreLink;
            public string LearnMoreLinkText;
            public string CloseButtonTitle;
        }
    }
}