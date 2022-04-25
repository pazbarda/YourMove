using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;
using YourMoveApp.server.api.exceptions;
using YourMoveApp.server.api.repositories;

namespace YourMoveApp.server
{
    internal class GameStateRepository : IGameStateRepository
    {
        private Dictionary<String, GameState> _gameIdToGameState = new();

        public String Save(GameState gameState)
        {
            _gameIdToGameState.Add(gameState.Id, gameState);
            return gameState.Id;
        }

        public GameState UpdateOrThrowException(String gameId, GameState gameState)
        {
            ValidateGameSavedOrThrowException(gameId);
            _gameIdToGameState[gameId] = gameState;
            return _gameIdToGameState[gameId];
        }

        public void Delete(String gameId)
        {
            ValidateGameSavedOrThrowException(gameId);
            _gameIdToGameState.Remove(gameId);
        }

        public GameState FindOrThrowException(String gameId)
        {
            ValidateGameSavedOrThrowException(gameId);
            return _gameIdToGameState[gameId];
        }

        public List<GameState> FindMultiple(List<String> gameIds)
        {
            List<GameState> result = new List<GameState>();
            foreach (String gameId in gameIds)
            {
                try
                {
                    result.Add(FindOrThrowException(gameId));
                }
                catch (Exception ex)
                {
                    // TODO PB - log warning with exception message
                }
            }
            return result;
        }

        private void ValidateGameSavedOrThrowException(String gameId)
        {
            if (!_gameIdToGameState.ContainsKey(gameId))
            {
                throw new ItemNotFoundException("no game saved with id=" + gameId);
            }
        }
    }
}
