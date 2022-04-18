using YourMoveApp.server;
using YourMoveApp.server.api;
using YourMoveApp.commons.model;
using YourMoveApp.server.api.repositories;

IGameStateRepository gameStateRpository = new GameStateRepository();
IMessageRepository messageRepository = new MessageRepository();
INotificationService notificationService = new NotificationService();
IGamesMatchingService gamesMatchingService = new GamesMatchingService(gameStateRpository, notificationService);
IMoveProcessingService moveProcessingService = new MoveProcessingService(gameStateRpository, notificationService);
IPlayerMessagingService playerMessagingService = new PlayerMessagingService(messageRepository, notificationService);

String gameId = gamesMatchingService.CreateNewGame("player-0");
gamesMatchingService.JoinGame(new JoinGameRequest("player-1", gameId));
List<Message> messages = playerMessagingService.GetAllUnreadMessagesForUser("player-0");
moveProcessingService.processMove(new Move(gameId, 0, 1, 'X'));
messages = playerMessagingService.GetAllUnreadMessagesForUser("player-1");
moveProcessingService.processMove(new Move(gameId, 1, 1, '0'));
messages = playerMessagingService.GetAllUnreadMessagesForUser("player-0");



/*
Action<object> callback = gamesMatchingService.GetAction();
notificationService.Register(EventType.GAME_STATE_CHANGE, callback);
notificationService.Notify(EventType.GAME_STATE_CHANGE, gameStateRpository.Find(gameId));
*/

