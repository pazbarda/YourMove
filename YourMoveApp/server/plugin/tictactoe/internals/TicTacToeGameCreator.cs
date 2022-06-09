using System.Collections.Generic;
using YourMoveApp.commons.model;
using YourMoveApp.commons.util;


namespace YourMoveApp.server.plugin.tictactoe
{
    class TicTacToeGameCreator
    {
        internal static GameState CreateNewGame(string initiatingPlayerId)
        {
            ObjectUtil.ValidateIdOrThrowException(initiatingPlayerId);
            Player initiatingPlayer = new(initiatingPlayerId, 'X');
            return new(CreateCleanBoard(), new List<Player> { initiatingPlayer });
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
    }

    
}
