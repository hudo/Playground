using System.Collections.Concurrent;

namespace Pipes
{
    public class SavePipe : IPipe<BlockingCollection<string>, Nothing>
    {
        public BlockingCollection<string> Input { set; private get; }
        public Nothing Output { set; private get; }
        public void Execute()
        {
            foreach (var item in Input)
            {
                //save data
            }
        }
    }
}