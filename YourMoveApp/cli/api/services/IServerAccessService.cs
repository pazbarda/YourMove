using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.cli.api.services
{
    interface IServerAccessService
    {
        public string CreateNewGame(CreateGameRequest createGameRequest);

        public List<Message> GetAllMessagesForUser(string userId);

        public List<Message> GetAllUnreadMessagesForUser(string userId);

        public List<GameType> GetGameTypes();

        public List<GameState> GetUnmatchedGames();

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest);

        public GenericResponse ProcessMove(Move move);
    }
}
