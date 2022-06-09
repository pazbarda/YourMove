using System.Collections.Generic;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;

namespace YourMoveApp.server
{
    internal class ServerController : IServerController
    {
        private readonly IGamesMatchingService _gamesMatchingService;
        private readonly IMoveProcessingService _moveProcessingService;
        private readonly IPlayerMessagingService _playerMessagingService;

        public ServerController(IGamesMatchingService gamesMatchingService, IMoveProcessingService moveProcessingService, IPlayerMessagingService playerMessagingService)
        {
            this._gamesMatchingService = gamesMatchingService;
            this._moveProcessingService = moveProcessingService;
            this._playerMessagingService = playerMessagingService;
        }

        public string CreateNewGame(CreateGameRequest createGameRequest)
        {
            return _gamesMatchingService.CreateNewGame(createGameRequest);
        }

        public List<GameState> GetUnmatchedGames()
        {
            return _gamesMatchingService.GetUnmatchedGames();
        }

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest)
        {
            return _gamesMatchingService.JoinGame(joinGameRequest);
        }

        public GenericResponse ProcessMove(Move move)
        {
            return _moveProcessingService.ProcessMove(move);
        }

        public List<Message> GetAllMessagesForUser(string userId)
        {
            return _playerMessagingService.GetAllMessagesForUser(userId);
        }

        public List<Message> GetAllUnreadMessagesForUser(string userId)
        {
            return _playerMessagingService.GetAllUnreadMessagesForUser(userId);
        }
    }
}
