using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;
using YourMoveApp.server.api.repositories;

namespace YourMoveApp.server
{
    internal class MoveProcessingService : IMoveProcessingService
    {
        private readonly IGameStateRepository _gameStateRepository;
        private readonly IGamePluginProvider _gamePluginProvider;
        private readonly INotificationService _notificationService;

        public MoveProcessingService(IGameStateRepository gameStateRepository, IGamePluginProvider gamePluginProvider, INotificationService notificationService)
        {
            this._gameStateRepository = gameStateRepository;
            this._gamePluginProvider = gamePluginProvider;
            this._notificationService = notificationService;
        }

        public void ProcessMove(Move move)
        {
            ValidateMove(move);
            GameState gameState = GetValidatedGameState(move.GameId);
            GameState newGameState = _gamePluginProvider.GetGamePlugin(gameState.GameType).ProcessMove(move, gameState);
            _gameStateRepository.Update(move.GameId, newGameState);
            _notificationService.Notify(EventType.GAME_STATE_CHANGE, newGameState);
        }

        private static void ValidateMove(Move move)
        {
            ValidateNotNull(move);
            ValidateNotNull(move.GameId);
        }

        private static void ValidateNotNull(Object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        private GameState GetValidatedGameState(String gameId)
        {
            GameState gameState = _gameStateRepository.Find(gameId);
            ValidateNotNull(gameState);
            return gameState;
        }
    }
}
