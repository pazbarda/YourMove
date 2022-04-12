using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.api
{
    internal interface IGameStateRpository
    {
        public String Save(GameState gameState);

        public GameState Update(String gameId, GameState gameState);

        public void Delete(String gameId);

        public GameState Find(String gameId);

        public List<GameState> FindMultiple(List<String> gameIds);

    }
}
