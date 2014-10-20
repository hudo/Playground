using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Pipes
{
    public class PipesTests
    {
        public void WireupTest()
        {
            var buffers = new Buffers();

            var tasks = ProcessBuilder.Create()
                .Pipe<BeginPipe, Nothing, BlockingCollection<string>>()
                    .Output(() => buffers.First)
                    .Wire
                .Pipe<ProcessPipe, BlockingCollection<string>, BlockingCollection<string>>()
                    .Input(() => buffers.First)
                    .Output(() => buffers.Second)
                    .Wire
                .Pipe<SavePipe, BlockingCollection<string>, Nothing>()
                    .Input(() => buffers.Second)
                    .Wire
                .Build();

            Task.WaitAll(tasks.ToArray());
        }
    }
}
