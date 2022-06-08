using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.server.api
{
    public delegate void Notification(object payload);

    public interface INotificationService
    {
        public void Register(EventType eventType, Notification callback);

        public void Notify(EventType eventType, object payload);
    }
}
