using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifications.Entities.Core
{
    public interface IEntityStateDetails
    {
        EntityStateDetailsType StateType { get; }
        IEntity Entity { get; set; }
    }
    public enum EntityStateDetailsType
    {
        Created,
        Updated,
        Deleted
    }


    public class CreatedEntityStateDetails : IEntityStateDetails
    {
        public EntityStateDetailsType StateType => EntityStateDetailsType.Created;

        public IEntity Entity { get; set; }
    }

    public class DeletedEntityStateDetails : IEntityStateDetails
    {
        public EntityStateDetailsType StateType => EntityStateDetailsType.Deleted;

        public IEntity Entity { get; set; }
    }

    public class UpdatedEntityStateDetails : IEntityStateDetails
    {
        public UpdatedEntityStateDetails()
        {
            Changes = new Dictionary<string, SimpleChange>();
        }
        public EntityStateDetailsType StateType => EntityStateDetailsType.Updated;

        public IEntity Entity { get; set; }

        public IEntity OriginalEntity { get; set; }

        public Dictionary<string, SimpleChange> Changes { get; set; }


    }

    public class SimpleChange
    {
        public object CurrentValue { get; set; }
        public object PreviousValue { get; set; }
    }
}
