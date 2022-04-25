using YourMoveApp.commons.model;

namespace YourMoveApp.server.plugin.tictactoe
{
    internal class TicTacToeMoveValidator
    {
        internal static readonly char EMPTY_CELL = '\0';

        internal static GenericResponse ValidateTicTacToeMove(Move move, GameState gameState)
        {
            if (!AreIndexesValid(move.X, move.Y, gameState.Board))
            {
                return new GenericResponse(false, "Indexes invalid: X=" + move.X  + ", Y=" + move.Y);
            }
            if (!IsCellEmpty(move.X, move.Y, gameState.Board))
            {
                return new GenericResponse(false, "Cell is not empty: X=" + move.X + ", Y=" + move.Y);
            }
            return new GenericResponse(true, "move processed successfully");
        }

        private static bool AreIndexesValid(int moveX, int moveY, char[][] board)
        {
            return (moveY < board.Length) && (moveY >= 0) && (moveX < board[moveY].Length) && (moveX >= 0);
        }

        private static bool IsCellEmpty(int moveX, int moveY, char[][] board)
        {
            return board[moveX][moveY] == EMPTY_CELL;
        }
    }
}
