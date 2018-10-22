using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddScoped<ServiceFactory>(x => x.GetService);

            services.Scan(scan => scan.FromCallingAssembly()
                .AddClasses(cls => cls.AssignableTo(typeof(ICommandHandler<,>))).AsImplementedInterfaces()
                .AddClasses(cls => cls.AssignableTo(typeof(IPipeline<,>))).AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ServiceFactory>();

            var response = await ComposeHandlers<TestCommand, Response>(new TestCommand(), factory);

            Console.WriteLine("Done.");
        }

        static Task<TResp> ComposeHandlers<TReq, TResp>(TReq request, ServiceFactory factory) where TReq : ICommand<TResp>
        {
            HandlerDelegate<TResp> handlerDelegate = () => factory.GetInstance<ICommandHandler<TReq, TResp>>().Handle(request);

            return factory
                .GetInstances<IPipeline<TReq, TResp>>()
                .Aggregate(handlerDelegate, (next, pipeline) => () => pipeline.Handle(request, next))();
                // creates a chain of delegates, each one with reference to next delegate (in Handle(..., next)). 
                // we just need to invoke the first one and the whole chain will execute one by one.
        }
    }

    // contracts and interfaces 
    
    public delegate object ServiceFactory(Type type);
    public delegate Task<TResp> HandlerDelegate<TResp>();
    
    public interface ICommand<TResp> { }
    public interface ICommandHandler<in TReq, TResp> where TReq : ICommand<TResp>
    {
        Task<TResp> Handle(TReq request);
    }

    public interface IPipeline<in TReq, TResp>
    {
        Task<TResp> Handle(TReq request, HandlerDelegate<TResp> next);
    }
    
    // test implementations
    
    public class Response { }

    public class TestCommand : ICommand<Response> { }
    
    public class TestCommandHandler : ICommandHandler<TestCommand, Response>
    {
        public Task<Response> Handle(TestCommand request)
        {
            Console.WriteLine("Test Handler Invoked");
            return Task.FromResult(new Response());
        }
    }
    
    public class TestPipeline : IPipeline<TestCommand, Response>
    {
        public async Task<Response> Handle(TestCommand request, HandlerDelegate<Response> next)
        {
            Console.WriteLine("Pipeline Invoked");
            return await next();
        }
    }

    public static class Ext
    {
        public static T GetInstance<T>(this ServiceFactory factory) => (T) factory(typeof(T));
        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory) => (IEnumerable<T>) factory(typeof(IEnumerable<T>));
    }
}