using System.Collections.Concurrent;

namespace Pipes
{
    public class Context
    {
        public BlockingCollection<string> First { get; set; }
        public BlockingCollection<string> Second { get; set; }
        public BlockingCollection<string> Third { get; set; }
    }
}