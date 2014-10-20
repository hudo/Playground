using System.Collections.Concurrent;

namespace Pipes
{
    public class SavePipe : IPipe<BlockingCollection<string>, NullStream>
    {
        public BlockingCollection<string> Input { set; private get; }
        public NullStream Output { set; private get; }
        public void Execute()
        {
            foreach (var item in Input)
            {
                //save data
            }
        }
    }
}