using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.plugin.tictactoe
{
    class TicTacToeGameCreator
    {
        internal static GameState CreateNewGame(String initiatingPlayerId)
        {
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
