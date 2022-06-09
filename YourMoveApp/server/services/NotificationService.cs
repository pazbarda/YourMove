using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.server.api;

// TODO PB -- unit tests [0]
namespace YourMoveApp.server
{
    public class NotificationService : INotificationService
    {
        private readonly Dictionary<EventType, NotificationChannel> _eventTypeToEventNotification = new();

        public NotificationService()
        {
            this._eventTypeToEventNotification = InitEventTypeToCallbacksList();
        }

        public void Notify(EventType eventType, object payload)
        {
            if (_eventTypeToEventNotification.ContainsKey(eventType))
            {
                _eventTypeToEventNotification[eventType].Notify(payload);
            }
        }

        public void Register(EventType eventType, Notification callback)
        {
            _eventTypeToEventNotification[eventType].Subscribe(callback);
        }

        public void Unregister(EventType eventType, Notification callback)
        {
            NotificationChannel notificationChannel = _eventTypeToEventNotification[eventType];
            if (notificationChannel != null)
            {
                notificationChannel.UnSubscribe(callback);
            }
        }

        private static Dictionary<EventType, NotificationChannel>  InitEventTypeToCallbacksList()
        {
            Dictionary<EventType, NotificationChannel> eventTypeToCallbacksList = new();
            foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
            {
                eventTypeToCallbacksList.Add(eventType, new NotificationChannel());
            }
            return eventTypeToCallbacksList;
        }


        class NotificationChannel
        {
            private event Notification _notificationEvent;

            internal void Notify(object payload)
            {
                if (this._notificationEvent != null)
                {
                    this._notificationEvent(payload);
                }
            }

            internal void Subscribe(Notification subscriber)
            {
                this._notificationEvent += subscriber;
            }
            internal void UnSubscribe(Notification subscriber)
            {
                this._notificationEvent -= subscriber;
            }
        }
    }
}
