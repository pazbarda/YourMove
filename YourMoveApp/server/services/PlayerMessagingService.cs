using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;
using YourMoveApp.server.api.repositories;

// TODO PB -- unit tests [1]
namespace YourMoveApp.server
{
    internal class PlayerMessagingService : IPlayerMessagingService
    {

        private readonly IMessageRepository _messageRepository;
        private readonly INotificationService _notificationService;

        public PlayerMessagingService(IMessageRepository messageRepository, INotificationService notificationService)
        {
            this._messageRepository = messageRepository;
            this._notificationService = notificationService;
            RegisterToNotifications();
        }

        public List<Message> GetAllMessagesForUser(string userId)
        {
            return _messageRepository.FindAllMessagesForUser(userId);
        }

        public List<Message> GetAllUnreadMessagesForUser(string userId)
        {
            return _messageRepository.FindAllNewMessagesForUser(userId);
        }

        public void Send(MessageInput messageInput)
        {
            _messageRepository.Save(new Message(
                    messageInput.RecepientId,
                    messageInput.Title,
                    messageInput.Text,
                    messageInput.Attachment
             ));
        }

        private void RegisterToNotifications()
        {
            _notificationService.Register(EventType.GAME_STATE_CHANGE, OnGameStateChange);
        }

        private void OnGameStateChange(object obj)
        {
            if (obj != null && obj is GameState)
            {
                GameState gameState = obj as GameState;
                string recepientId = gameState.NextPlayer.UserId;

                MessageInput messageInput = new(
                        recepientId,
                        "It's you Move! -- game id = " + gameState.Id,
                        "It's your turn to play!",
                        gameState
                    );
                Send(messageInput);
            }

        }
    }
}
