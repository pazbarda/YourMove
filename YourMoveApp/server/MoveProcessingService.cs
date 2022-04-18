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
        private readonly INotificationService notificationService;

        public MoveProcessingService(IGameStateRepository gameStateRepository, INotificationService notificationService)
        {
            this._gameStateRepository = gameStateRepository;
            this.notificationService = notificationService;
        }

        public void processMove(Move move)
        {
            ValidateMove(move);
            GameState gameState = _gameStateRepository.Find(move.GameId);
            ValidateNotNull(gameState);
            char[][] updatedBoard = UpdateBoard(gameState.Board, move);
            GameState newGameState = new GameState.Cloner(gameState)
                .With(updatedBoard)
                .Clone();
            newGameState.AdvancePlayer();
            _gameStateRepository.Update(move.GameId, newGameState);
            notificationService.Notify(EventType.GAME_STATE_CHANGE, newGameState);
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

        private static char[][] UpdateBoard(char[][] board, Move move)
        {
            board[move.Y][move.X] = move.GameCharacter;
            return board;
        }
    }
}
