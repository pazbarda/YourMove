using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.plugin.tictactoe
{
    internal class TicTacToeMoveProcessor
    {
        internal static GameState ProcessTicTacToeMove(Move move, GameState gameState)
        {
            char moveGameChar = move.GameCharacter;
            int moveX = move.X;
            int moveY = move.Y;

            char[][] updatedBoard = GetUpdatedBoard(move, gameState);

            GameState updatedGameState = new GameState.Cloner(gameState).With(updatedBoard).Clone();

            if (
                CheckHorizontalForWin(moveX, moveY, updatedBoard) ||
                CheckVerticalForWin(moveX, moveY, updatedBoard) ||
                CheckPrimaryDiagnoalForWin(moveX, moveY, updatedBoard) ||
                CheckSecondaryDiagnoalForWin(moveX, moveY, updatedBoard)
                )
            {
                updatedGameState.GameStatus = GameStatus.WIN;
            }
            else
            {
                updatedGameState.AdvancePlayer();
            }
            return updatedGameState;
        }

        private static char[][] GetUpdatedBoard(Move move, GameState gameState)
        {
            char[][] board = gameState.Board;
            board[move.Y][move.X] = move.GameCharacter;
            return board;
        }

        private static bool CheckHorizontalForWin(int moveX, int moveY, char[][] updatedBoard)
        {
            char gameChar = updatedBoard[moveX][moveY];
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
            char gameChar = updatedBoard[moveX][moveY];
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
            char gameChar = updatedBoard[moveX][moveY];
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
            char gameChar = updatedBoard[moveX][moveY];
            int k = updatedBoard[moveY].Length;
            while (k >= 0)
            {
                if (updatedBoard[k][k] != gameChar)
                {
                    return false;
                }
                k--;
            }
            return true;
        }
    }
}
