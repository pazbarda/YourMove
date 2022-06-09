using System;
using YourMoveApp.commons.model;

namespace YourMoveApp.commons.plugin
{
    public interface IGamePlugin
    {
        public GameState CreateGame(string initiatingPlayerId);

        public GameState JoinGame(string joiningPlayerId, GameState gameState);

        public GameState ProcessMove(Move move, GameState gameState);

        public GenericResponse ValidateMove(Move move, GameState gameState);

        public Func<Move, GameState, GenericResponse> GetMoveValiator() { return ValidateMove; }
    }
}
