using YourMoveApp.commons.plugin;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.api
{
    public interface IGamePluginProvider
    {
        public IGamePlugin GetGamePlugin(GameType gameType);
    }
}
