using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifications.Entities.Core
{
    interface IEntityStateNotificationObjectBuilder
    {
        CrawlableEntity GenerateNotificationObject(IEntityStateDetails entityStateDetails);
    }
}
