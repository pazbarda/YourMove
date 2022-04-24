using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;

namespace YourMoveApp.server
{
    internal class GamesMatchingService : IGamesMatchingService
    {
        private readonly HashSet<String> _unmatchedGameIds = new();

        private readonly IRepository<GameState> _gameStateRepository;
        private readonly IGamePluginProvider _gamePluginProvider;
        private readonly INotificationService _notificationService;

        public GamesMatchingService(IRepository<GameState> gameStateRepository, IGamePluginProvider gamePluginProvider, INotificationService notificationService)
        {
            this._gameStateRepository = gameStateRepository;
            this._gamePluginProvider = gamePluginProvider;
            this._notificationService = notificationService;
        }

        public string CreateNewGame(CreateGameRequest createGameRequest)
        {
            GameState gameState = _gamePluginProvider.GetGamePlugin(createGameRequest.GameType).CreateGame(createGameRequest.UserId);
            gameState.GameStatus = GameStatus.UMATCHED;
            String newGameId = _gameStateRepository.Save(gameState);
            _unmatchedGameIds.Add(newGameId);
            return newGameId;
        }

        public List<GameState> GetUnmatchedGames()
        {
            List<String> sortedUmatchedGameIds = GetSortedUmatchedGameIds();
            List<GameState> unmatchedGameStates = new();
            sortedUmatchedGameIds.ForEach(gameId => {
                GameState gameState = _gameStateRepository.Find(gameId);
                if (gameState != null)
                {
                    unmatchedGameStates.Add(gameState);
                }
            });
            return unmatchedGameStates;
        }

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest)
        {
            // TODO PB -- clean this method, use exception catching for fail flows instead of if/else
            String gameId = joinGameRequest.GameId;
            if (!_unmatchedGameIds.Contains(gameId))
            {
                return new GenericResponse(false, "no unmatched game found with id " + gameId + ", might already be matched");
            }
            _unmatchedGameIds.Remove(gameId);
            GameState gameState = _gameStateRepository.Find(gameId);
            if (null == gameState)
            {
                return new GenericResponse(false, "no game found with id " + gameId);
            }
            GameState newGameState = _gamePluginProvider.GetGamePlugin(gameState.GameType).JoinGame(gameId, gameState);
            newGameState.GameStatus = GameStatus.ONGOING;
            _gameStateRepository.Update(gameId, newGameState);
            _notificationService.Notify(EventType.GAME_STATE_CHANGE, newGameState);
            return new GenericResponse(true, "user " + joinGameRequest.UserId + " joined game " + newGameState.Id);
        }

        private List<String> GetSortedUmatchedGameIds()
        {
            List<String> sortedUmatchedGameIds = _unmatchedGameIds.ToList();
            sortedUmatchedGameIds.Sort();
            return sortedUmatchedGameIds;
        }
    }
}
