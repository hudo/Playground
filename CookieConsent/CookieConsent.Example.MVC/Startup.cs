using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CookieConsent.Example.MVC.Startup))]
namespace CookieConsent.Example.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
