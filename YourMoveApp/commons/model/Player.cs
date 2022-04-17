using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    public class Player
    {
        public String UserId { get; set; }
        public char GameCharacter { get; set; }

        public Player(String id, char gameCharacter)
        {
            this.UserId = id;
            this.GameCharacter = gameCharacter;
        }
    }
}
