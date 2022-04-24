using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;

namespace YourMoveApp.commons.plugin
{
    public interface IGamePlugin
    {
        public GameState CreateGame(String initiatingPlayerId);

        public GameState JoinGame(String joiningPlayerId, GameState gameState);

        public GameState ProcessMove(Move move, GameState gameState);

        public GenericResponse ValidateMove(Move move, GameState gameState);
    }
}
