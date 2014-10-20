using System;

namespace Pipes
{
    public class ProcessBuilder
    {
        private ProcessBuilder()
        {
        }

        public static ProcessBuilder Wireup()
        {
            return new ProcessBuilder();
        }

        public PipeBuilder<P, I, O> Pipe<P, I, O>() where P : IPipe<I, O>, new()
        {
            return new PipeBuilder<P, I, O>(this);
        }

        public void Go()
        {

        }


        public class PipeBuilder<P, I, O> where P : IPipe<I, O>, new()
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

            public ProcessBuilder FinishPipe
            {
                get { return _processBuilder; }
            }
        }
    }
}