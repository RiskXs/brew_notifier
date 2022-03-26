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

        public StateChangedNotifier(INotificationWriter notificationWriter)
        {
            _notificationWriter = notificationWriter;
        }
        public Task OnStateChanges(IEntity entity, IEntity originalEntity)
        {
            var company = (CompanyEntity)entity;
            var originalCompany = (CompanyEntity)originalEntity;

            if (!ShouldNotify(company, originalCompany))
            {
                return _notificationWriter.Notify(entity);
            }

            return Task.CompletedTask;
        }

        private bool ShouldNotify(CompanyEntity company, CompanyEntity originalCompany)
        {
            return
                IsNewCompany(company, originalCompany) ||
                IsCompanyDeleted(company, originalCompany) ||
                isEntityDeletionStatusChanged(company, originalCompany) ||
                isCrawlingStatusChangedSignificantly(company, originalCompany);
        }

        private bool IsNewCompany(CompanyEntity entity, CompanyEntity originalEntity)
        {
            return entity != null && originalEntity == null;
        }

        private bool IsCompanyDeleted(CompanyEntity entity, CompanyEntity originalEntity)
        {
            return entity == null && originalEntity != null;
        }

        private bool isEntityDeletionStatusChanged(CompanyEntity entity, CompanyEntity originalEntity)
        {
            return entity.IsDeleted != originalEntity.IsDeleted;
        }

        private bool isCrawlingStatusChangedSignificantly(CompanyEntity entity, CompanyEntity originalEntity)
        {
            return
                entity.CrawlingStatus != originalEntity.CrawlingStatus &&
                (entity.CrawlingStatus == CrawlingStatus.TEXT_ANALYZED || entity.CrawlingStatus == CrawlingStatus.TEXT_UPLOADED);
        }
    }
}
