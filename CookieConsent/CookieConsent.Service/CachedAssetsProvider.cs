namespace CookieConsent.Service
{
    public class CachedAssetsProvider : IAssetsProvider
    {
        private static string _htmlElement;
        private static string _js;

        public CachedAssetsProvider(IFileShim fileShim, ConsentSettings settings)
        {
            _htmlElement = fileShim.ReadAllText(settings.HtmlFileLocation);
            _js = fileShim.ReadAllText(settings.JsFileLocation);
        }

        public string HtmlElement
        {
            get { return _htmlElement; }
        }

        public string Javascript
        {
            get { return _js; }
        }
    }
}