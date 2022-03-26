using BrewNotifications.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifier.Entities.BL
{
    public class ChangeStateValidators
    {
        public static IChangeStateValidator IsEntityCreated { get { return new EntityCreateStateValidator(); } }
        public static IChangeStateValidator IsEntityDeleted { get { return new EntityDeletedStateValidator(); } }
        public static IChangeStateValidator IsEntityMarkedForDeletion { get { return new EntityMarkedForDeletionChangeStateValidator(); } }
        public static IChangeStateValidator IsEntityCrawlingStatusChangedSignificantly { get { return new CrawlingStatusChangedSignificantlyValidator(); } }
    }

    public class EntityCreateStateValidator : IChangeStateValidator
    {
        public bool ValidateChange(IEntity entity, IEntity originalEntity)
        {
            return entity != null && originalEntity == null;
        }
    }
    public class EntityDeletedStateValidator : IChangeStateValidator
    {
        public bool ValidateChange(IEntity entity, IEntity originalEntity)
        {
            return entity == null && originalEntity != null;
        }
    }

    public class EntityMarkedForDeletionChangeStateValidator : IChangeStateValidator
    {
        public bool ValidateChange(IEntity entity, IEntity originalEntity)
        {
            var crawlableEntity = entity as CrawlableEntity;
            var origianlCrawlableEntity = originalEntity as CrawlableEntity;
            return
                (crawlableEntity != null && origianlCrawlableEntity != null) &&
                crawlableEntity.IsDeleted != origianlCrawlableEntity.IsDeleted;
        }
    }

    public class CrawlingStatusChangedSignificantlyValidator : IChangeStateValidator
    {
        public bool ValidateChange(IEntity entity, IEntity originalEntity)
        {
            var crawlableEntity = entity as CrawlableEntity;
            var origianlCrawlableEntity = originalEntity as CrawlableEntity;
            return
                (crawlableEntity != null && origianlCrawlableEntity != null) &&
                (crawlableEntity.CrawlingStatus == CrawlingStatus.TEXT_ANALYZED || crawlableEntity.CrawlingStatus == CrawlingStatus.TEXT_UPLOADED);
        }
    }

}
