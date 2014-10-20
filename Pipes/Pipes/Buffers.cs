using System.Collections.Concurrent;

namespace Pipes
{
    public class Buffers
    {
        public BlockingCollection<string> First { get; set; }
        public BlockingCollection<string> Second { get; set; }
        public BlockingCollection<string> Third { get; set; }
    }
}