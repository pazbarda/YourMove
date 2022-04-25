using YourMoveApp.commons.model;
using YourMoveApp.server.api;
using YourMoveApp.server.api.exceptions;

// TODO PB -- unit tests [1]
namespace YourMoveApp.server
{
    public class GamesMatchingService : IGamesMatchingService
    {
        private readonly HashSet<string> _unmatchedGameIds = new();

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
            string newGameId = _gameStateRepository.Save(gameState);
            _unmatchedGameIds.Add(newGameId);
            return newGameId;
        }

        public List<GameState> GetUnmatchedGames()
        {
            List<string> sortedUmatchedGameIds = GetSortedUmatchedGameIds();
            List<GameState> unmatchedGameStates = new();
            sortedUmatchedGameIds.ForEach(gameId => {
                try
                {
                    unmatchedGameStates.Add(_gameStateRepository.FindOrThrowException(gameId));
                }
                catch (Exception ex)
                {
                    // TODO PB -- log warning with exception message
                }
            });
            return unmatchedGameStates;
        }

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest)
        {
            try
            {
                return JoinGameOrThrowException(joinGameRequest);
            }
            catch (ItemNotFoundException ex)
            {
                return new GenericResponse(false, ex.Message);
            }
        }

        private List<string> GetSortedUmatchedGameIds()
        {
            List<string> sortedUmatchedGameIds = _unmatchedGameIds.ToList();
            sortedUmatchedGameIds.Sort();
            return sortedUmatchedGameIds;
        }

        private GenericResponse JoinGameOrThrowException(JoinGameRequest joinGameRequest)
        {
            string gameId = joinGameRequest.GameId;
            GameState gameState = GetUmatchedGameStateOrThrowException(gameId);
            GameState newGameState = _gamePluginProvider.GetGamePlugin(gameState.GameType).JoinGame(gameId, gameState);
            newGameState.GameStatus = GameStatus.ONGOING;
            _gameStateRepository.UpdateOrThrowException(gameId, newGameState);
            _notificationService.Notify(EventType.GAME_STATE_CHANGE, newGameState);
            return new GenericResponse(true, "user " + joinGameRequest.UserId + " joined game " + newGameState.Id);
        }

        private GameState GetUmatchedGameStateOrThrowException(string gameId)
        {
            ValidateUnmatchedGameExistsOrThrowException(gameId);
            _unmatchedGameIds.Remove(gameId);
            return _gameStateRepository.FindOrThrowException(gameId);
        }

        private void ValidateUnmatchedGameExistsOrThrowException(string gameId)
        {
            if (!_unmatchedGameIds.Contains(gameId))
            {
                throw new ItemNotFoundException("no unmatched game found with id " + gameId + ", might already be matched");
            }
        }
    }
}
