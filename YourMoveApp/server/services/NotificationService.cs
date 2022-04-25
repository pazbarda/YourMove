using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.server.api;

// TODO PB -- unit tests [0]
namespace YourMoveApp.server
{
    internal class NotificationService : INotificationService
    {
        private readonly Dictionary<EventType, List<Action<object>>> _eventTypeToCallbacksList = new();

        public NotificationService()
        {
            this._eventTypeToCallbacksList = InitEventTypeToCallbacksList();
        }

        public void Notify(EventType eventType, object payload)
        {
            foreach (Action<object> action in _eventTypeToCallbacksList[eventType])
            {
                action(payload);
            }
        }

        public void Register(EventType eventType, Action<object> callback)
        {
            _eventTypeToCallbacksList[eventType].Add(callback);
        }

        private static Dictionary<EventType, List<Action<object>>>  InitEventTypeToCallbacksList()
        {
            Dictionary<EventType, List<Action<object>>> eventTypeToCallbacksList = new();
            foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
            {
                eventTypeToCallbacksList.Add(eventType, new());
            }
            return eventTypeToCallbacksList;
        }
    }
}
