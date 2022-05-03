using YourMoveApp.commons.model;

namespace YourMoveApp.server.api
{
    internal interface IServerController
    {
        public string CreateNewGame(CreateGameRequest createGameRequest);

        public GenericResponse JoinGame(JoinGameRequest joinGameRequest);

        public List<GameState> GetUnmatchedGames();

        public GenericResponse ProcessMove(Move move);

        public List<Message> GetAllUnreadMessagesForUser(string userId);

        public List<Message> GetAllMessagesForUser(string userId);
    }
}
