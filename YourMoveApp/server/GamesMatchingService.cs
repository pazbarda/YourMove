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
        private readonly INotificationService _notificationService;

        public GamesMatchingService(IRepository<GameState> gameStateRpository, INotificationService notificationService)
        {
            this._gameStateRepository = gameStateRpository;
            this._notificationService = notificationService;
        }

        public string CreateNewGame(string initiatingPlayerId)
        {
            Player initiatingPlayer = new(initiatingPlayerId, 'X');
            GameState gameState = new(GameStatus.UMATCHED, CreateCleanBoard(), new List<Player> { initiatingPlayer });
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
            Player newPlayer = new(joinGameRequest.UserId, 'O');
            GameState newGameState = updateAndGetGameState(gameState, newPlayer);
            _notificationService.Notify(EventType.GAME_STATE_CHANGE, newGameState);
            return new GenericResponse(true, "user " + newPlayer.UserId + " joined game " + newGameState.Id + " as " + newPlayer.GameCharacter);
        }

        private static char[][] CreateCleanBoard()
        {
            char[][] board = new char[3][];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new char[3];
            }
            return board;
        }

        private List<String> GetSortedUmatchedGameIds()
        {
            List<String> sortedUmatchedGameIds = _unmatchedGameIds.ToList();
            sortedUmatchedGameIds.Sort();
            return sortedUmatchedGameIds;
        }

        private GameState updateAndGetGameState(GameState oldGameState, Player newPlayer)
        {
            GameState newGameState = new GameState.Cloner(oldGameState).Clone();
            newGameState.AddPlayer(newPlayer);
            newGameState.GameStatus = GameStatus.ONGOING;
            return _gameStateRepository.Update(oldGameState.Id, newGameState);
        }
    }
}
