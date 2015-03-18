using System.Threading.Tasks;
using Microsoft.Owin;

namespace SimpleThrottler
{
    public class ThrottlingMiddleware : OwinMiddleware
    {

        public ThrottlingMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            
            await Next.Invoke(context);
        }
    }
}
