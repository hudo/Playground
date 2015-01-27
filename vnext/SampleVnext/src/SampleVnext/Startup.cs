using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.IO;
using System.Threading.Tasks;

namespace SampleVnext
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

			app.UseStaticFiles();
			app.UseWelcomePage("/welcome");

			app.UseServices(services => {
				
			});

			app.Use(next => context => FilterAsync(context, next));

			app.Use(async (ctx, next) =>
			{
				await ctx.Response.WriteAsync("hello");
				await next();
			});

			app.UseMiddleware<Middleware>();
		}

		public async Task FilterAsync(HttpContext context, RequestDelegate next)
		{
			var body = context.Response.Body;
			var buffer = new MemoryStream();
			context.Response.Body = buffer;

			try
			{
				context.Response.Headers["CustomHeader"] = "My Header";

				await context.Response.WriteAsync("Before\r\n");
				await next(context);
				await context.Response.WriteAsync("After\r\n");

				buffer.Position = 0;
				await buffer.CopyToAsync(body);
			}
			finally
			{
				context.Response.Body = body;
			}
		}
	}

	public class Middleware
	{
		private RequestDelegate _next;

		public Middleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			await httpContext.Response.WriteAsync(" world");
			await _next.Invoke(httpContext);
		}
	}
}
