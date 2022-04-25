using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.server.api
{
    public interface INotificationService
    {
        public void Register(EventType eventType, Action<object> callback);

        public void Notify(EventType eventType, object payload);
    }
}
