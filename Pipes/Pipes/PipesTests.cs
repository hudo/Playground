using System.Collections.Concurrent;

namespace Pipes
{
    public class PipesTests
    {
        public void WireupTest()
        {
            var context = new Context();

            ProcessBuilder.Wireup()
                .Pipe<BeginPipe, NullStream, BlockingCollection<string>>()
                    .Output(() => context.First)
                    .FinishPipe
                .Pipe<ProcessPipe, BlockingCollection<string>, BlockingCollection<string>>()
                    .Input(() => context.First)
                    .Output(() => context.Second)
                    .FinishPipe
                .Pipe<SavePipe, BlockingCollection<string>, NullStream>()
                    .Input(() => context.Second)
                    .FinishPipe
                .Go();

        }
    }
}
