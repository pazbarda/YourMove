using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class CreateGameRequest
    {
        public string UserId { get; }
        public GameType GameType { get; }

        public CreateGameRequest(String userId, GameType gameType)
        {
            this.UserId = userId;
            this.GameType = gameType;
        }
    }
}
