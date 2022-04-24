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
        public Func<Move, GameState, GameState> GetMoveProcessor();

        public Func<Move, GameState, GenericResponse> ValidateMove();
    }
}
