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

        public GenericResponse ProcessMove(Move move)
        {
            try
            {
                ProcessMoveOrThrowException(move);
                return new GenericResponse(true, "move processed successfully");
            }
            catch (Exception ex)
            {
                return new GenericResponse(false, ex.Message);
            }
        }

        private void ProcessMoveOrThrowException(Move move)
        {
            ValidateMoveOrThrowException(move);
            GameState gameState = GetGameStateOrThrowException(move.GameId);
            GameState newGameState = _gamePluginProvider.GetGamePlugin(gameState.GameType).ProcessMove(move, gameState);
            _gameStateRepository.UpdateOrThrowException(move.GameId, newGameState);
            _notificationService.Notify(EventType.GAME_STATE_CHANGE, newGameState);
        }

        private static void ValidateMoveOrThrowException(Move move)
        {
            ValidateNotNullOrThrowException(move);
            ValidateNotNullOrThrowException(move.GameId);
        }

        private static void ValidateNotNullOrThrowException(Object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        private GameState GetGameStateOrThrowException(String gameId)
        {
            GameState gameState = _gameStateRepository.FindOrThrowException(gameId);
            ValidateNotNullOrThrowException(gameState);
            return gameState;
        }
    }
}
