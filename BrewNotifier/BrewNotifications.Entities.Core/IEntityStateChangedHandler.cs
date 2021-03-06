using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifications.Entities.Core
{
    public interface IEntityStateChangedHandler
    {
        Task OnStateChanges(IEntity entity, IEntity originalEntity);
    }
}
