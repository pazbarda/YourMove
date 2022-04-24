using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.plugin.tictactoe
{
    internal class TicTacToeGameJoiner
    {
        internal static GameState JoinGame(string joiningPlayerId, GameState gameState)
        {
            Player newPlayer = new(joiningPlayerId, 'O');
            GameState newGameState = new GameState.Cloner(gameState).Clone();
            newGameState.AddPlayer(newPlayer);
            return newGameState;
        }
    }
}
