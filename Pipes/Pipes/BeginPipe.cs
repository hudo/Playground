using System.Collections.Concurrent;

namespace Pipes
{
    public class BeginPipe : IPipe<NullStream, BlockingCollection<string>>
    {
        public NullStream Input { set; private get; }
        public BlockingCollection<string> Output { set; private get; }
        public void Execute()
        {
            Output.Add("data");
            Output.CompleteAdding();
        }
    }
}