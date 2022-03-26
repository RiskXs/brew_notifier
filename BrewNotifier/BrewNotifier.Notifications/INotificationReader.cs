using System;
using System.Threading.Tasks;

namespace BrewNotifier.Notifications
{
    public interface INotificationReader
    {
        Task<object> CheckForNotification();
    }
}
