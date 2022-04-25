using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.commons.plugin;
using YourMoveApp.server.api;
using YourMoveApp.server.plugin.tictactoe;

namespace YourMoveApp.server
{
    public class GamePluginProvider : IGamePluginProvider
    {
        private readonly Dictionary<GameType, IGamePlugin> _gameTypeToGamePlugin = GetGameTypeToGamePluginDictionary();

        public IGamePlugin GetGamePlugin(GameType gameType)
        {
            // TODO PB -- throw custom exception if gameType doesnt exist
            return _gameTypeToGamePlugin[gameType];
        }

        private static Dictionary<GameType, IGamePlugin> GetGameTypeToGamePluginDictionary()
        {
            return new Dictionary<GameType, IGamePlugin>
            {
                { GameType.TIC_TAC_TOE, new TicTacToeGamePlugin() }
            };
        }
    }
}
