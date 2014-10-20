using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pipes
{
    public class ProcessBuilder
    {
        private List<Action> PipeExecutionCallbacks; 

        private ProcessBuilder()
        {
            PipeExecutionCallbacks = new List<Action>();
        }

        public static ProcessBuilder Create()
        {
            return new ProcessBuilder();
        }

        public PipeBuilder<P, I, O> Pipe<P, I, O>()
            where P : IPipe<I, O>, new()
            where O : class
            where I : class
        {
            return new PipeBuilder<P, I, O>(this);
        }

        public IEnumerable<Task> Build()
        {
            var taskFactory = new TaskFactory();

            return PipeExecutionCallbacks.Select(taskFactory.StartNew);
        }

        public class PipeBuilder<P, I, O> 
            where P : IPipe<I, O>, new()
            where I: class
            where O: class
        {
            private readonly ProcessBuilder _processBuilder;
            private P _instance;

            public PipeBuilder(ProcessBuilder processBuilder)
            {
                _processBuilder = processBuilder;
                _instance = new P();
            }

            public PipeBuilder<P, I, O> Input(Func<I> inputStream)
            {
                _instance.Input = inputStream();
                return this;
            }

            public PipeBuilder<P, I, O> Output(Func<O> outputStream)
            {
                _instance.Output = outputStream();
                return this;
            }

            public ProcessBuilder Wire
            {
                get
                {
                    _processBuilder.PipeExecutionCallbacks.Add(() => _instance.Execute());
                    return _processBuilder;
                }
            }
        }
    }
}