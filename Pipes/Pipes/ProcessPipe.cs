using System.Collections.Concurrent;

namespace Pipes
{
    public class ProcessPipe : IPipe<BlockingCollection<string>, BlockingCollection<string>>
    {
        public BlockingCollection<string> Input { set; private get; }
        public BlockingCollection<string> Output { set; private get; }
        public void Execute()
        {
            foreach (var item in Input)
            {
                Output.Add(item);
            }

            Output.CompleteAdding();
        }
    }
}