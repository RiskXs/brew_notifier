using BrewNotifications.Entities.Core;
using BrewNotifier.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifications.Entities.Company
{
    public class StateChangedNotifier : IEntityStateChangedHandler
    {
        private readonly INotificationWriter _notificationWriter;
        private readonly IEnumerable<IChangeStateValidator> _notificationConditions;

        public StateChangedNotifier(INotificationWriter notificationWriter, IEnumerable<IChangeStateValidator> notificationConditions)
        {
            _notificationWriter = notificationWriter;
            _notificationConditions = notificationConditions;
        }
        public Task OnStateChanges(IEntity entity, IEntity originalEntity)
        {
            var company = (CompanyEntity)entity;
            var originalCompany = (CompanyEntity)originalEntity;

            if (!ShouldNotify(company, originalCompany))
            {
                return Task.CompletedTask;
            }

            var notifiedEntity = company ?? originalCompany;
            return _notificationWriter.Notify(notifiedEntity);
        }

        private bool ShouldNotify(IEntity entity, IEntity originalEntity)
        {
            return _notificationConditions.Any(validator => validator.ValidateChange(entity, originalEntity));
        }
    }
}
