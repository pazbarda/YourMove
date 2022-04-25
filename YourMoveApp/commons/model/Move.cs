using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    public class Move
    {
        public string GameId { get; }
        public int X { get; }
        public int Y { get; }
        public char GameCharacter { get; }

        public Move(string gameId, int x, int y, char gameCharacter)
        {
            this.GameId = gameId;
            this.X = x;
            this.Y = y;
            this.GameCharacter = gameCharacter;
        }
    }
}
