using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;

namespace YourMoveApp.server
{
    internal class GameStateRepository : IGameStateRpository
    {
        private Dictionary<String, GameState> _gameIdToGameState = new();

        public String Save(GameState gameState)
        {
            String newGameId = Guid.NewGuid().ToString("N");
            _gameIdToGameState.Add(newGameId, gameState);
            return newGameId;
        }

        public GameState Update(String gameId, GameState gameState)
        {
            _gameIdToGameState.Add(gameId, gameState);
            return _gameIdToGameState[gameId];
        }

        public void Delete(String gameId)
        {
            _gameIdToGameState.Remove(gameId);
        }

        public GameState Find(String gameId)
        {
            return _gameIdToGameState[gameId];
        }

        public List<GameState> FindMultiple(List<String> gameIds)
        {
            List<GameState> result = new List<GameState>();
            foreach (String gameId in gameIds)
            {
                GameState gameState = Find(gameId);
                if (gameState != null)
                {
                    result.Add(gameState);
                }
            }
            return result;
        }
    }
}
