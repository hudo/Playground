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
            // aspnet core ioc/di container
            var services = new ServiceCollection();

            // cool trick of registering one method (delegate) into IOC container, no need for a class!
            services.AddScoped<ServiceFactory>(x => x.GetService);

            // scan for implementations with Scrutor
            services.Scan(scan => scan.FromCallingAssembly()
                .AddClasses(cls => cls.AssignableTo(typeof(ICommandHandler<,>))).AsImplementedInterfaces()
                .AddClasses(cls => cls.AssignableTo(typeof(IPipeline<,>))).AsImplementedInterfaces()
                .AddClasses(cls => cls.AssignableTo(typeof(IPrePipelineStep<>))).AsImplementedInterfaces()
                .AddClasses(cls => cls.AssignableTo(typeof(IPostPipelineStep<,>))).AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ServiceFactory>();

            // poor mans MediatR:
            var response = await ComposeHandlers<TestCommand, Response>(new TestCommand(), factory);

            Console.WriteLine("Done.");
            
            // Result:
            // Pipeline step Invoked
            // Test Handler Invoked
        }

        static Task<TResp> ComposeHandlers<TReq, TResp>(TReq request, ServiceFactory factory) where TReq : ICommand<TResp>
        {
            // the magic happens here. 
            HandlerDelegate<TResp> handlerDelegate = () => factory.GetInstance<ICommandHandler<TReq, TResp>>().Handle(request);

            return factory
                .GetInstances<IPipeline<TReq, TResp>>()
                .Reverse()
                .Aggregate(handlerDelegate, (next, pipeline) => () => pipeline.Handle(request, next))();
                // creates a chain of delegates, each one with reference to next delegate (in Handle(..., next)). 
                // we just need to invoke the first one and the whole chain will execute one by one.
                // pipeline 1 --> pipeline 2 --> command handler
        }
    }

    // contracts and interfaces 
    
    public delegate object ServiceFactory(Type type); // ioc 
    public delegate Task<TResp> HandlerDelegate<TResp>(); // handler or pipeline method
    
    public interface ICommand<TResp> { }
    public interface ICommandHandler<in TReq, TResp> where TReq : ICommand<TResp>
    {
        Task<TResp> Handle(TReq request);
    }

    // pipeline

    public interface IPipeline<in TReq, TResp>
    {
        Task<TResp> Handle(TReq request, HandlerDelegate<TResp> next);
    }

    public interface IPrePipelineStep<in TReq>
    {
        Task Process(TReq request);
    }

    public interface IPostPipelineStep<in TReq, in TResp>
    {
        Task Process(TReq request, TResp response);
    }

    // internal implementation for pre/post steps

    public class PreHandlerStep<TReq, TResp> : IPipeline<TReq, TResp>
    {
        private readonly IEnumerable<IPrePipelineStep<TReq>> _steps;

        public PreHandlerStep(IEnumerable<IPrePipelineStep<TReq>> steps)
        {
            _steps = steps;
        }

        public async Task<TResp> Handle(TReq request, HandlerDelegate<TResp> next)
        {
            foreach (var step in _steps)
            {
                await step.Process(request);
            }

            return await next();
        }
    }

    public class PostHandlerStep<TReq, TResp> : IPipeline<TReq, TResp>
    {
        private readonly IEnumerable<IPostPipelineStep<TReq, TResp>> _steps;

        public PostHandlerStep(IEnumerable<IPostPipelineStep<TReq, TResp>> steps)
        {
            _steps = steps;
        }

        public async Task<TResp> Handle(TReq request, HandlerDelegate<TResp> next)
        {
            var response = await next();

            foreach (var step in _steps)
            {
                await step.Process(request, response);
            }

            return response;
        }
    }

    // sample implementations
    
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
    
    public class PipelineStep : IPipeline<TestCommand, Response>
    {
        public async Task<Response> Handle(TestCommand request, HandlerDelegate<Response> next)
        {
            Console.WriteLine("Pipeline step Invoked");
            return await next();
        }
    }

    public class TestPreStep : IPrePipelineStep<TestCommand>
    {
        public Task Process(TestCommand request)
        {
            Console.WriteLine("Pre test step");
            return Task.CompletedTask;
        }
    }

    public class TestPostStep : IPostPipelineStep<TestCommand, Response>
    {
        public Task Process(TestCommand request, Response response)
        {
            Console.WriteLine("Post test step");
            return Task.CompletedTask;
        }
    }

    // for convenience
    
    public static class Ext
    {
        public static T GetInstance<T>(this ServiceFactory factory) => (T) factory(typeof(T));
        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory) => (IEnumerable<T>) factory(typeof(IEnumerable<T>));
    }
}