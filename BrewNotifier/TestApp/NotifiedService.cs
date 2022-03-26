using BrewNotifier.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TestApp
{
    public class NotifiedService
    {
        private readonly INotificationReader _notificationReader;

        public NotifiedService(INotificationReader notificationReader)
        {
            _notificationReader = notificationReader;
        }

        public async Task Run()
        {
            Console.WriteLine("NotifiedService Says: Listening for notifications....");
            while (true)
            {
                var notification = await _notificationReader.CheckForNotification();
                if(notification == null)
                {
                    continue;
                }

                string json = JsonSerializer.Serialize(notification);
                Console.WriteLine($"NotifiedService Says: I got new notification: \n {json} \n");
            }
        }
    }
}
