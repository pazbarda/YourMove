using YourMoveApp.server;
using YourMoveApp.server.api;
using YourMoveApp.commons.model;

IRepository<GameState> gameStateRpository = new GameStateRepository();
INotificationService notificationService = new NotificationService();
IGamesMatchingService gamesMatchingService = new GamesMatchingService(gameStateRpository, notificationService);
IMoveProcessingService moveProcessingService = new MoveProcessingService(gameStateRpository);

String gameId = gamesMatchingService.CreateNewGame("player-0");
gamesMatchingService.JoinGame(new JoinGameRequest("player-1", gameId));

moveProcessingService.processMove(new Move(gameId, 0, 1, 'X'));

/*
Action<object> callback = gamesMatchingService.GetAction();
notificationService.Register(EventType.GAME_STATE_CHANGE, callback);
notificationService.Notify(EventType.GAME_STATE_CHANGE, gameStateRpository.Find(gameId));
*/

