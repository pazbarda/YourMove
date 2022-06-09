using YourMoveApp.server;
using YourMoveApp.server.api;
using YourMoveApp.server.api.repositories;
using YourMoveApp.commons.model;
using YourMoveApp.commons.plugin;
using YourMoveApp.commons.util;
using System;

/*IGameStateRepository gameStateRepository = new GameStateRepository();
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
messages = playerMessagingService.GetAllUnreadMessagesForUser("player-0");*/

/*
List<Func<int>> inputFuncs = new();
inputFuncs.Add(new Func<int>(() => {
    Task.Delay(1000).Wait();
    Console.WriteLine("Program -- returning 100");
    return 100;
}));
inputFuncs.Add(new Func<int>(() => {
    Task.Delay(2000).Wait();
    Console.WriteLine("Program -- returning 20");
    return 20;
}));
inputFuncs.Add(new Func<int>(() => {
    Task.Delay(3000).Wait();
    Console.WriteLine("Program -- returning 3");
    return 3;
}));

Func<List<int>, int> resultCalculationFunc = new Func<List<int>, int>(ints => {
    int sum = 0;
    foreach (int n in ints)
    {
        sum += n;
    }
    return sum;
});

int result = await new MultiAsyncCalculator<int, int>(inputFuncs, resultCalculationFunc).CalculateAsync();
Console.WriteLine(result);*/

namespace YourMoveApp
{
    /*delegate void Notification(string str);

    class Notifier
    {
        private event Notification notificationEvent;

        public void Notify(String message)
        {
            if (this.notificationEvent != null)
            {
                this.notificationEvent(message);
            }
        }

        public void Subscribe(Notification subscriber)
        {
            this.notificationEvent += subscriber;
        }
        public void UnSubscribe(Notification subscriber)
        {
            this.notificationEvent -= subscriber;
        }
    }*/

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("main program is running");

            /*Notifier eventEmitter = new();

            eventEmitter.Subscribe(subscriber1);
            eventEmitter.Subscribe(subscriber2);
            eventEmitter.Notify("JAMBALAYA!!!");

            eventEmitter.UnSubscribe(subscriber1);
            eventEmitter.Notify("RUSTY!!!");

            eventEmitter.UnSubscribe(subscriber1);
            eventEmitter.UnSubscribe(subscriber2);
            eventEmitter.Notify("STELLAAAAA!!!");*/

            NotificationService notificationService = new NotificationService();

            notificationService.Register(EventType.GAME_STATE_CHANGE, subscriber1);
            notificationService.Notify(EventType.GAME_STATE_CHANGE, "HELLOOO!!!");
            notificationService.Register(EventType.GAME_STATE_CHANGE, subscriber2);
            notificationService.Notify(EventType.GAME_STATE_CHANGE, "STELLAAAAAAAA!!!");
            notificationService.Unregister(EventType.GAME_STATE_CHANGE, subscriber1);
            notificationService.Unregister(EventType.GAME_STATE_CHANGE, subscriber2);
            notificationService.Notify(EventType.GAME_STATE_CHANGE, "NEWMAN!!!");
        }

        static void subscriber1(object payload)
        {
            Console.WriteLine("subscriber1 received event with " + payload.ToString());
        }

        static void subscriber2(object payload)
        {
            Console.WriteLine("subscriber2 received event with " + payload.ToString());
        }
    }
}

