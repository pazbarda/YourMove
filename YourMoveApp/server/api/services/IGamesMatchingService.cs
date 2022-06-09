using System.Collections.Generic;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.api
{
    public interface IGamesMatchingService
    {
        public string CreateNewGame(CreateGameRequest createGameRequest);

        public List<GameState> GetUnmatchedGames();

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest);
    }
}
