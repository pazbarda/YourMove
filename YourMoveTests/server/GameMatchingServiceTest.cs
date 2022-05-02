using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using YourMoveApp.commons.model;
using YourMoveApp.commons.plugin;
using YourMoveApp.server;
using YourMoveApp.server.api;

namespace YourMoveTests
{
    [TestClass]
    internal class GameMatchingServiceTest
    {
        private readonly Mock<IRepository<GameState>> gameStateRepositoryMock = new Mock<IRepository<GameState>>();
        private readonly Mock<IGamePluginProvider> gamePluginProviderMock = new Mock<IGamePluginProvider>();
        private readonly Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
        private readonly Mock<IGamePlugin> gamePluginMock = new Mock<IGamePlugin>();

        private static readonly string INITIATING_PLAYER_ID = "123";

        [TestMethod]
        public void TestCreateNewGame_valid()
        {
            IGamesMatchingService gamesMatchingServiceUnderTest = new GamesMatchingService(
                gameStateRepositoryMock.Object,
                gamePluginProviderMock.Object,
                notificationServiceMock.Object
                );

            gamePluginProviderMock.Setup(mock => mock.GetGamePlugin(GameType.TIC_TAC_TOE)).Returns(gamePluginMock.Object);

            gamesMatchingServiceUnderTest.CreateNewGame(new CreateGameRequest(INITIATING_PLAYER_ID, GameType.TIC_TAC_TOE));
        }
    }
}
