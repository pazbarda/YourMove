using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class JoinGameRequest
    {
        public string UserId { get; }
        public string GameId { get; }

        public JoinGameRequest(String userId, String gameId)
        {
            this.UserId = userId;
            this.GameId = gameId;
        }
    }
}
