using System;

namespace SimpleThrottler.Engine
{
    public struct RequestEvent
    {
        public DateTime Time { get; set; }
        public string Address { get; set; }
        public int Count { get; set; }
    }
}