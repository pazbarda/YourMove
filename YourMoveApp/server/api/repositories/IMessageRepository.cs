using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.api.repositories
{
    internal interface IMessageRepository : IRepository<Message>
    {
        List<Message> FindAllMessagesForUser(string userId);

        List<Message> FindAllNewMessagesForUser(string userId);

        void MarkMessageAsRead(string messageId);
    }
}
