using System;

namespace CookieConsent.Service
{
    public class Wireup
    {
        public static SettingsWireup Init()
        {
            return new SettingsWireup();
        }

        public class SettingsWireup
        {
            internal static ConsentSettings Settings;

            static SettingsWireup()
            {
                Settings = new ConsentSettings();
            }

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
                if(Settings.LocalizedContentSettings.ContainsKey("default"))
                    throw new Exception("Default localized content is already configured.");

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
}