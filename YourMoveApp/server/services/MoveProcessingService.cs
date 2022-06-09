using System;
using YourMoveApp.commons.model;
using YourMoveApp.commons.util;
using YourMoveApp.server.api;
using YourMoveApp.server.api.repositories;

// TODO PB -- unit tests [1]
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
            ObjectUtil.ValidateNotNullOrThrowException(move);
            ObjectUtil.ValidateNotNullOrThrowException(move.GameId);
        }

        private GameState GetGameStateOrThrowException(String gameId)
        {
            GameState gameState = _gameStateRepository.FindOrThrowException(gameId);
            ObjectUtil.ValidateNotNullOrThrowException(gameState);
            return gameState;
        }
    }
}
