using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.api
{
    internal interface IPlayerMessagingService
    {
        public void Send(MessageInput messageInput);

        public List<Message> GetAllUnreadMessagesForUser(string userId);

        public List<Message> GetAllMessagesForUser(string userId);
    }
}
