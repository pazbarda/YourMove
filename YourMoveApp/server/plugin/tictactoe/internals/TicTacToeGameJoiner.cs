using YourMoveApp.commons.model;
using YourMoveApp.commons.util;

namespace YourMoveApp.server.plugin.tictactoe
{
    internal class TicTacToeGameJoiner
    {
        internal static GameState JoinGame(string joiningPlayerId, GameState gameState)
        {
            ObjectUtil.ValidateNotNullOrThrowException(gameState);
            ObjectUtil.ValidateIdOrThrowException(joiningPlayerId);
            Player newPlayer = new(joiningPlayerId, 'O');
            GameState newGameState = new GameState.Cloner(gameState).Clone();
            newGameState.AddPlayer(newPlayer);
            return newGameState;
        }
    }
}
