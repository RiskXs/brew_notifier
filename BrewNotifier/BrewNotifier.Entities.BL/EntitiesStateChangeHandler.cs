using BrewNotifications.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewNotifier.Entities.BL
{
    public interface IEntitiesStateChangeHandler
    {
        Task OnEntityStateChanged(IEntity entity, IEntity originalEntity, string entityType);
    }
    public class EntitiesStateChangeHandler: IEntitiesStateChangeHandler
    {
        private readonly Dictionary<string, IEntityStateChangedHandler> _entityTypesHandler;

        public EntitiesStateChangeHandler(Dictionary<string, IEntityStateChangedHandler> entityTypesHandler)
        {
            _entityTypesHandler = entityTypesHandler;
        }

        public Task OnEntityStateChanged(IEntity entity, IEntity originalEntity, string entityType)
        {
            var stateChangedHandler = _entityTypesHandler.GetValueOrDefault(entityType.ToLower());
            if(stateChangedHandler == null)
            {
                return Task.CompletedTask;
            }

            return stateChangedHandler.OnStateChanges(entity, originalEntity);
        }
    }
}
