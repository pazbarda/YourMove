using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
