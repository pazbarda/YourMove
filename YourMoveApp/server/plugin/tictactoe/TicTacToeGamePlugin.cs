using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.commons.plugin;
using YourMoveApp.server.plugin.tictactoe;

// TODO PB -- unit tests [0]
namespace YourMoveApp.server.plugin.tictactoe
{
    public class TicTacToeGamePlugin : IGamePlugin
    {
        public GameState CreateGame(string initiatingPlayerId)
        {
           return TicTacToeGameCreator.CreateNewGame(initiatingPlayerId);
        }

        public GameState JoinGame(string joiningPlayerId, GameState gameState)
        {
            return TicTacToeGameJoiner.JoinGame(joiningPlayerId, gameState);
        }

        public GameState ProcessMove(Move move, GameState gameState)
        {
            return TicTacToeMoveProcessor.ProcessTicTacToeMove(move, gameState);
        }

        public GenericResponse ValidateMove(Move move, GameState gameState)
        {
            return TicTacToeMoveValidator.ValidateTicTacToeMove(move, gameState);
        }
    }
}
