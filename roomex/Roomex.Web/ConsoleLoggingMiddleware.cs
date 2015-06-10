using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Roomex.Web
{
    /// <summary>
    /// Logs application requests info Debug window
    /// </summary>
    public class ConsoleLoggingMiddleware : OwinMiddleware
    {
        public ConsoleLoggingMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            await Next.Invoke(context);
            
            stopwatch.Stop();
            Debug.WriteLine("{0} Request: {1}, elapsed {2} ms", DateTime.Now.ToShortTimeString(), context.Request.Uri, stopwatch.ElapsedMilliseconds);
        }
    }
}