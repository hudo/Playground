namespace CookieConsent.Service
{
    public class AssetsResource : IAssetsResource
    {
        protected static string Html;
        protected static string Js;

        static AssetsResource()
        {
            Html = @"";

            Js = @"";
        }

        public string HtmlTemplate
        {
            get { return Html; }
        }

        public string Javascript
        {
            get { return Js; }
        }
    }
}