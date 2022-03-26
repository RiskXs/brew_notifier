using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifier.Notifications
{
    public interface INotificationWriter
    {
        Task Notify(object obj);
    }
}
