using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.server.api;
using YourMoveApp.cli.api.services;
using YourMoveApp.commons.model;

namespace YourMoveApp.cli.services
{
    class ServerAccessService : IServerAccessService
    {
        private readonly IServerController _serverController;

        public ServerAccessService(IServerController serverController)
        {
            this._serverController = serverController;
        }

        public string CreateNewGame(CreateGameRequest createGameRequest)
        {
            return _serverController.CreateNewGame(createGameRequest);
        }

        public List<Message> GetAllMessagesForUser(string userId)
        {
            return _serverController.GetAllMessagesForUser(userId);
        }

        public List<Message> GetAllUnreadMessagesForUser(string userId)
        {
            return _serverController.GetAllUnreadMessagesForUser(userId);
        }

        public List<GameType> GetGameTypes()
        {
            return Enum.GetValues(typeof(GameType)).Cast<GameType>().ToList();
        }

        public List<GameState> GetUnmatchedGames()
        {
            return _serverController.GetUnmatchedGames();
        }

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest)
        {
            return _serverController.JoinGame(joinGameRequest);
        }

        public GenericResponse ProcessMove(Move move)
        {
            return _serverController.ProcessMove(move);
        }
    }
}
