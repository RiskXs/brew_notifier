using BrewNotifications.Entities.Core;
using BrewNotifier.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifications.Entities.CompanyCompetitor
{
    public class CompanyCompetitorStateChangedNotifier: IEntityStateChangedHandler
    {
        private readonly INotificationWriter _notificationWriter;
        private readonly IEnumerable<IChangeStateValidator> _notificationConditions;

        public CompanyCompetitorStateChangedNotifier(INotificationWriter notificationWriter, IEnumerable<IChangeStateValidator> notificationConditions)
        {
            _notificationWriter = notificationWriter;
            _notificationConditions = notificationConditions;
        }
        public Task OnStateChanges(IEntity entity, IEntity originalEntity)
        {
            var companyCompetitor = (CompanyCompetitorEntity)entity;
            var originalCompanyCompetitor = (CompanyCompetitorEntity)originalEntity;

            if (!ShouldNotify(companyCompetitor, originalCompanyCompetitor))
            {
                return Task.CompletedTask;
            }

            return _notificationWriter.Notify(companyCompetitor.Company);
        }

        private bool ShouldNotify(IEntity entity, IEntity originalEntity)
        {
            return _notificationConditions.Any(validator => validator.ValidateChange(entity, originalEntity));
        }
    }
}
