using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting.Messaging;

namespace CookieConsent.Service
{
    public class Wireup
    {
        public static SettingsWireup Init()
        {
            return new SettingsWireup() { Settings = new ConsentSettings() };
        }

        public class SettingsWireup
        {
            public ConsentSettings Settings;

            public SettingsWireup WithLocalizedContent(string culture, string title, string description, string learnMoreTitle, string closeTitle, string learnMoreUrl = null)
            {
                Settings.LocalizedContentSettings.Add(culture,
                    new ConsentSettings.LocalizedContent()
                    {
                        Title = title,
                        Description = description,
                        LearnMoreLink = learnMoreTitle,
                        LearnMoreLinkText = learnMoreUrl,
                        CloseButtonTitle = closeTitle
                    });
                
                return this;
            }

            public SettingsWireup WithDefaultContent(string title, string description, string learnMoreTitle, string closeTitle, string learnMoreUrl = null)
            {
                Settings.FallbackCulture = "default";
                return WithLocalizedContent("default", title, description, learnMoreTitle, closeTitle, learnMoreUrl);
            }

            public SettingsWireup SetDefaultFallbackCulture(string culture)
            {
                Settings.FallbackCulture = culture;
                return this;
            }
        }
    }

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

        public string JsFileLocation;
        public string HtmlFileLocation;
        public Dictionary<string, LocalizedContent> LocalizedContentSettings;
        public string FallbackCulture;

        public Dictionary<string, string> GetMappings(string culture)
        {
            if(!LocalizedContentSettings.ContainsKey(culture))
                throw new ArgumentException("Unknown culture");

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