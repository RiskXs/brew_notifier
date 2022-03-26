using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BrewNotifier.Notifications.InMemeory
{
    public class NotificationsQueue: INotificationReader, INotificationWriter
    {
        private ConcurrentQueue<object> _queue;
        public NotificationsQueue()
        {
            _queue = new ConcurrentQueue<object>();
        }

        public Task<object> CheckForNotification()
        {
            object result;
            _queue.TryDequeue(out result);

            return Task.FromResult(result);
        }

        public Task Notify(object obj)
        {
            _queue.Enqueue(obj);
            return Task.CompletedTask;
        }
    }
}
