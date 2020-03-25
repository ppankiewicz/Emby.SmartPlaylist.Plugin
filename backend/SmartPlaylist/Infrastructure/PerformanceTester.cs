using System;
using System.Diagnostics;

namespace SmartPlaylist.Infrastructure
{
    public class PerformanceTester : IDisposable
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Action<TimeSpan> _callback;

        public PerformanceTester()
        {
            _stopwatch.Start();
        }

        public PerformanceTester(Action<TimeSpan> callback) : this()
        {
            _callback = callback;
        }

        public static PerformanceTester Start(Action<TimeSpan> callback)
        {
            return new PerformanceTester(callback);
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _callback?.Invoke(Result);
        }
        public TimeSpan Result => _stopwatch.Elapsed;
    }
}