using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CookieConsent.Aspnet;
using CookieConsent.Service;

namespace CookieConsent.Example.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AspnetCookieConsent.Settings = Wireup.Init()
                .WithDefaultContent("TITLE", "DESC", "LEARN MORE", "CLOSE")
                .WithLocalizedContent("hr-HR", "TITLE HR", "DESC HR", "LEARN MORE HR", "CLOSE HR")
                .GetSettings();

        }
    }
}
