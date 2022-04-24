using YourMoveApp.server;
using YourMoveApp.server.api;
using YourMoveApp.server.api.repositories;
using YourMoveApp.commons.model;
using YourMoveApp.commons.plugin;

IGameStateRepository gameStateRepository = new GameStateRepository();
IMessageRepository messageRepository = new MessageRepository();
INotificationService notificationService = new NotificationService();
IGamePluginProvider gamePluginProvider = new GamePluginProvider();
IGamesMatchingService gamesMatchingService = new GamesMatchingService(gameStateRepository, gamePluginProvider, notificationService);
IMoveProcessingService moveProcessingService = new MoveProcessingService(gameStateRepository, gamePluginProvider, notificationService);
IPlayerMessagingService playerMessagingService = new PlayerMessagingService(messageRepository, notificationService);

String gameId = gamesMatchingService.CreateNewGame(new CreateGameRequest("player-0", GameType.TIC_TAC_TOE));
gamesMatchingService.JoinGame(new JoinGameRequest("player-1", gameId));
List<Message> messages = playerMessagingService.GetAllUnreadMessagesForUser("player-0");
moveProcessingService.ProcessMove(new Move(gameId, 0, 1, 'X'));
messages = playerMessagingService.GetAllUnreadMessagesForUser("player-1");
moveProcessingService.ProcessMove(new Move(gameId, 1, 1, '0'));
messages = playerMessagingService.GetAllUnreadMessagesForUser("player-0");

