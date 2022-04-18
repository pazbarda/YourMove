using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.server.api;
using YourMoveApp.commons.model;
using YourMoveApp.server.api.repositories;

namespace YourMoveApp.server
{
    internal class MessageRepository : IMessageRepository
    {
        private readonly Dictionary<string, Message> _messageIdToMessage = new();
        private readonly Dictionary<string, List<String>> _userIdToMessageIds = new();

        public void Delete(string messageId)
        {
            Message message = _messageIdToMessage[messageId];
            if (null!=message)
            {
                DeleteMessage(message);
            }
        }

        public Message Find(string messageId)
        {
            return _messageIdToMessage[messageId];
        }

        public List<Message> FindAllMessagesForUser(string userId)
        {
            List<Message> messages = new List<Message>();
            if (_userIdToMessageIds.ContainsKey(userId))
            {
                List<string> userMessageIds = _userIdToMessageIds[userId];
                foreach (string messageId in userMessageIds)
                {
                    Message message = Find(messageId);
                    if (null!=message)
                    {
                        messages.Add(message);
                    }
                }
            }
            return messages;
        }

        public List<Message> FindAllNewMessagesForUser(string userId)
        {
            List<Message> messages = new List<Message>();
            foreach(Message message in FindAllMessagesForUser(userId))
            {
                if (!message.IsRead)
                {
                    messages.Add(message);
                }
            }
            return messages;
        }

        public List<Message> FindMultiple(List<string> messageIds)
        {
            List<Message> result = new List<Message>();
            foreach (String messageId in messageIds)
            {
                Message message = Find(messageId);
                if (message != null)
                {
                    result.Add(message);
                }
            }
            return result;
        }

        public string Save(Message message)
        {
            string messageId = message.Id;
            _messageIdToMessage.Add(messageId, message);
            string receipientId = message.RecepientId;
            AddUserIfNotExists(receipientId);
            _userIdToMessageIds[receipientId].Add(messageId);
            return messageId;
        }

        public Message Update(string messageId, Message message)
        {
            _messageIdToMessage[messageId] = message;
            return _messageIdToMessage[messageId];
        }

        public void MarkMessageAsRead(string messageId)
        {
            if (_messageIdToMessage.ContainsKey(messageId))
            {
                _messageIdToMessage[messageId].MarkAsRead();
            }
        }

        private void DeleteMessage(Message message)
        {
            string userId = message.RecepientId;
            string messageId = message.Id;
            deleteMessageForUser(userId, messageId);
            _messageIdToMessage.Remove(messageId);
        }

        private void AddUserIfNotExists(string userId)
        {
            if (!_userIdToMessageIds.ContainsKey(userId))
            {
                _userIdToMessageIds[userId] = new();
            }
        }

        private void deleteMessageForUser(string userId, string messageId)
        {
            if (_userIdToMessageIds.ContainsKey(userId))
            {
                _userIdToMessageIds[userId].Remove(messageId);
            }
        }
    }
}
