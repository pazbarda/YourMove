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


        public Object Clone()
        {
            return new Player(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is Player player &&
                   UserId == player.UserId &&
                   GameCharacter == player.GameCharacter;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, GameCharacter);
        }

        private Player(Player player) : this(player.UserId, player.GameCharacter)
        {}
    }
}
