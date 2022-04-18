using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    public class Player : ICloneable
    {
        public String UserId { get; set; }
        public char GameCharacter { get; set; }

        public Player(String UserId, char gameCharacter)
        {
            this.UserId = UserId;
            this.GameCharacter = gameCharacter;
        }

        private Player(Player player) : this(player.UserId, player.GameCharacter)
        {}

        public Object Clone()
        {
            return new Player(this);
        }
    }
}
