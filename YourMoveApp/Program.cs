using YourMoveApp.server;
using YourMoveApp.server.api;
using YourMoveApp.commons.model;

IGameStateRpository gameStateRpository = new GameStateRepository();
IGamesMatchingService gamesMatchingService = new GamesMatchingService(gameStateRpository);
IMoveProcessingService moveProcessingService = new MoveProcessingService(gameStateRpository);

String gameId = gamesMatchingService.CreateNewGame("player-0");

moveProcessingService.processMove(new Move(gameId, 0, 1, 'X'));

