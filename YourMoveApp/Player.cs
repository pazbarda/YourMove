using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class Player
    {
        private int Id { get; set; }
        private char GameCharacter { get; set; }

        public Player(int id, char gameCharacter)
        {
            this.Id = id;
            this.GameCharacter = gameCharacter;
        }
    }
}
