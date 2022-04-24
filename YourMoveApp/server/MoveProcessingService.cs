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
        private readonly INotificationService _notificationService;
        private readonly IGamePluginProvider _gamePluginProvider;

        public MoveProcessingService(IGameStateRepository gameStateRepository, INotificationService notificationService, IGamePluginProvider gamePluginProvider)
        {
            this._gameStateRepository = gameStateRepository;
            this._notificationService = notificationService;
            this._gamePluginProvider = gamePluginProvider;
        }

        public void processMove(Move move)
        {
            ValidateMove(move);
            GameState gameState = getValidatedGameState(move.GameId);
            GameState newGameState = _gamePluginProvider.GetGamePlugin(gameState.GameType).GetMoveProcessor().Invoke(move, gameState);
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

        private GameState getValidatedGameState(String gameId)
        {
            GameState gameState = _gameStateRepository.Find(gameId);
            ValidateNotNull(gameState);
            return gameState;
        }
    }
}
