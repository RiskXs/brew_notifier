using BrewNotifications.Entities.Company;
using BrewNotifications.Entities.Core;
using BrewNotifier.Entities.BL;
using BrewNotifier.Notifications.InMemeory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        private static NotificationsQueue notificationsQueue = new NotificationsQueue();
        static void Main(string[] args)
        {
            IEntitiesStateChangeHandler entitiesStateChangeHandler = InitializeEntitiesChangeHandler();
            ChangeStatusService changeStatusService = new ChangeStatusService(entitiesStateChangeHandler);
            NotifiedService notifiedService = new NotifiedService(notificationsQueue);
            Task.WaitAll(
                Task.Run(() => changeStatusService.Run()),
                Task.Run(() => notifiedService.Run()));
        }

        static IEntitiesStateChangeHandler InitializeEntitiesChangeHandler()
        {
            Dictionary<string, IEntityStateChangedHandler> entityTypesHandler = new Dictionary<string, IEntityStateChangedHandler>
            {
                {CompanyEntity.EntityName,  new StateChangedNotifier(notificationsQueue)}
            };

            return new EntitiesStateChangeHandler(entityTypesHandler);
        }
    }
}
