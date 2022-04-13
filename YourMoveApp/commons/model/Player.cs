using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    public class Player
    {
        private String Id { get; set; }
        private char GameCharacter { get; set; }

        public Player(String id, char gameCharacter)
        {
            this.Id = id;
            this.GameCharacter = gameCharacter;
        }
    }
}
