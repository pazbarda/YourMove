using YourMoveApp.commons.model;
using YourMoveApp.commons.util;

namespace YourMoveApp.server.plugin.tictactoe
{
    internal class TicTacToeMoveProcessor
    {
        internal static GameState ProcessTicTacToeMove(Move move, GameState gameState)
        {
            ValidateMoveOrThrowException(move, gameState);
            ObjectUtil.ValidateNotNullOrThrowException(gameState);
            int moveX = move.X;
            int moveY = move.Y;
            char[][] updatedBoard = GetUpdatedBoard(move, gameState);
            GameState updatedGameState = new GameState.Cloner(gameState).With(updatedBoard).Clone();

            if (CheckForWin(moveX, moveY, updatedBoard)) {
                updatedGameState.GameStatus = GameStatus.WIN;
            }
            else if (CheckForFullyOccupiedBoard(updatedBoard))
            {
                updatedGameState.GameStatus = GameStatus.TIE;
            }
            else
            {
                updatedGameState.AdvancePlayer();
            }
            return updatedGameState;
        }

        private static void ValidateMoveOrThrowException(Move move, GameState gameState)
        {
            ObjectUtil.ValidateNotNullOrThrowException(move);
            if (move.Y < 0 || move.Y > gameState.Board.Length - 1)
            {
                throw new ArgumentException("move destination Y value is invalid: " + move.Y);
            }
            if (move.X < 0 || move.X > gameState.Board[0].Length - 1)
            {
                throw new ArgumentException("move destination Y value is invalid: " + move.Y);
            }
        }

        private static char[][] GetUpdatedBoard(Move move, GameState gameState)
        {
            char[][] board = gameState.Board;
            board[move.Y][move.X] = move.GameCharacter;
            return board;
        }

        private static bool CheckForWin(int moveX, int moveY, char[][] updatedBoard)
        {
            return CheckHorizontalForWin(moveX, moveY, updatedBoard) ||
                CheckVerticalForWin(moveX, moveY, updatedBoard) ||
                CheckPrimaryDiagnoalForWin(moveX, moveY, updatedBoard) ||
                CheckSecondaryDiagnoalForWin(moveX, moveY, updatedBoard);
        }

        private static bool CheckHorizontalForWin(int moveX, int moveY, char[][] updatedBoard)
        {
            char gameChar = updatedBoard[moveY][moveX];
            int x = 0;
            while (x < updatedBoard[moveY].Length)
            {
                if (updatedBoard[moveY][x] != gameChar)
                {
                    return false;
                }
                x++;
            }
            return true;
        }

        private static bool CheckVerticalForWin(int moveX, int moveY, char[][] updatedBoard)
        {
            char gameChar = updatedBoard[moveY][moveX];
            int y = 0;
            while (y < updatedBoard.Length)
            {
                if (updatedBoard[y][moveX] != gameChar)
                {
                    return false;
                }
                y++;
            }
            return true;
        }

        private static bool CheckPrimaryDiagnoalForWin(int moveX, int moveY, char[][] updatedBoard)
        {
            char gameChar = updatedBoard[moveY][moveX];
            int k = 0;
            while (k < updatedBoard.Length)
            {
                if (updatedBoard[k][k] != gameChar)
                {
                    return false;
                }
                k++;
            }
            return true;
        }

        private static bool CheckSecondaryDiagnoalForWin(int moveX, int moveY, char[][] updatedBoard)
        {
            char gameChar = updatedBoard[moveY][moveX];
            int y = 0;
            int x = updatedBoard[y].Length - 1;
            while (y < updatedBoard.Length && x >= 0)
            {
                if (updatedBoard[y][x] != gameChar)
                {
                    return false;
                }
                y++;
                x--;
            }
            return true;
        }

        private static bool CheckForFullyOccupiedBoard(char[][] updatedBoard)
        {
            for (int row=0; row<updatedBoard.Length; row++)
            {
                for (int col = 0; col < updatedBoard[row].Length; col++)
                {
                    if (updatedBoard[row][col] == TicTacToeMoveValidator.EMPTY_CELL)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
