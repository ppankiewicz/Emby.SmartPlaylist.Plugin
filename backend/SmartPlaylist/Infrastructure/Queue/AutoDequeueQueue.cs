using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartPlaylist.Infrastructure.Queue
{
    public class AutoDequeueQueue<T> : IDisposable
    {
        private readonly AutoDequeueQueueConfig _config;
        private readonly Action<IEnumerable<T>> _onDequeue;
        private readonly List<T> _items = new List<T>();
        private readonly object _lock = new object();
        private readonly TimeSpan _period = TimeSpan.FromHours(1);
        private Timer _absoluteTimer;
        private Timer _timer;

        public AutoDequeueQueue(Action<IEnumerable<T>> onDequeue, AutoDequeueQueueConfig config)
        {
            _onDequeue = onDequeue;
            _config = config;
        }

        public void Dispose()
        {
            StopTimer();
        }

        public void Enqueue(T item)
        {
            lock (_lock)
            {
                if (_items.Count < _config.MaxItemsLimit)
                {
                    StartOrUpdateTimer();

                    _items.Add(item);
                }
                else
                {
                    StopTimer();
                    OnTimerCallback(null);
                }
            }
        }

        private void StartOrUpdateTimer()
        {
            if (_timer == null)
            {
                _timer = CreateTimer(_config.InactiveDequeueTime);
                _absoluteTimer = CreateTimer(_config.AbsoluteDequeueTime);
            }
            else
            {
                _timer.Change(_config.InactiveDequeueTime, _period);
            }
        }

        private Timer CreateTimer(TimeSpan dueTime)
        {
            return new Timer(OnTimerCallback, null, dueTime, _period);
        }

        private void OnTimerCallback(object state)
        {
            lock (_lock)
            {
                Dequeue();
            }
        }

        private void Dequeue()
        {
            StopTimer();
            var items = _items.ToList();
            Task.Run(() => _onDequeue(items));
            _items.Clear();
        }

        private void StopTimer()
        {
            _absoluteTimer?.Dispose();
            _timer?.Dispose();
            _timer = null;
            _absoluteTimer = null;
        }
    }
}