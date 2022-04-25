using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using YourMoveApp.server.plugin.tictactoe;
using YourMoveApp.commons.model;
using System;

namespace YourMoveTests.plugin
{
    [TestClass]
    public class TicTacToePluginTest
    {
        private readonly TicTacToeGamePlugin pluginUnderTest = new();

        private static readonly string initiatingPlayerId = "123";
        private static readonly string joiningPlayerId = "456";        

        [TestMethod]
        public void TestCreateGame_valid()
        {
            GameState expectedGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X') });
            GameState actualGameState = pluginUnderTest.CreateGame(initiatingPlayerId);
            Assert.IsNotNull(actualGameState);
            Assert.AreEqual(expectedGameState, actualGameState);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGame_nullPlayerId()
        {
            pluginUnderTest.CreateGame(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGame_emptyPlayerId()
        {
            pluginUnderTest.CreateGame("");
        }

        [TestMethod]
        public void TestJoinGame_valid()
        {
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X') });
            GameState expectedGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            GameState actualGameState = pluginUnderTest.JoinGame(joiningPlayerId, initialGameState);
            Assert.IsNotNull(actualGameState);
            Assert.AreEqual(expectedGameState, actualGameState);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestJoinGame_nullPlayerId()
        {
            string initiatingPlayerId = "123";
            string joiningPlayerId = null;
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X') });
            pluginUnderTest.JoinGame(joiningPlayerId, initialGameState);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestJoinGame_emptyPlayerId()
        {
            string initiatingPlayerId = "123";
            string joiningPlayerId = "";
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X') });
            pluginUnderTest.JoinGame(joiningPlayerId, initialGameState);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJoinGame_nullGameState()
        {
            string joiningPlayerId = "456";
            GameState initialGameState = null;
            pluginUnderTest.JoinGame(joiningPlayerId, initialGameState);
        }

        [TestMethod]
        public void TestProcessMove_valid()
        {
            TestMove(new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') }), 0, 0, 'X');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestProcessMove_nullMove()
        {
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            Move move = null;
            pluginUnderTest.ProcessMove(move, initialGameState);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProcessMove_invalidMove_Xnegative()
        {
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            Move move = new(initialGameState.Id, -1, 0, 'X');
            pluginUnderTest.ProcessMove(move, initialGameState);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProcessMove_invalidMove_XtooHigh()
        {
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            Move move = new(initialGameState.Id, 5, 0, 'X');
            pluginUnderTest.ProcessMove(move, initialGameState);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProcessMove_invalidMove_Ynegative()
        {
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            Move move = new(initialGameState.Id, 0, -1, 'X');
            pluginUnderTest.ProcessMove(move, initialGameState);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProcessMove_invalidMove_YtooHigh()
        {
            GameState initialGameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            Move move = new(initialGameState.Id, 0, -5, 'X');
            pluginUnderTest.ProcessMove(move, initialGameState);
        }

        [TestMethod]
        public void TestProcessMove_fullGame_winVertical()
        {
           /*
            x o x
            . o .
            x o .
             */
            GameState gameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            gameState = TestMove(gameState, 0, 0, 'X');
            gameState = TestMove(gameState, 1, 1, 'O');
            gameState = TestMove(gameState, 2, 0, 'X');
            gameState = TestMove(gameState, 1, 0, 'O');
            gameState = TestMove(gameState, 0, 2, 'X');
            TestFinalMove(gameState, 1, 2, 'O', GameStatus.WIN);
        }

        public void TestProcessMove_fullGame_winHorizontal()
        {
            /*
            x x x
            . o .
            . o .
             */

            GameState gameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            gameState = TestMove(gameState, 0, 0, 'X');
            gameState = TestMove(gameState, 1, 1, 'O');
            gameState = TestMove(gameState, 2, 0, 'X');
            gameState = TestMove(gameState, 1, 2, 'O');
            TestFinalMove(gameState, 1, 0, 'X', GameStatus.WIN);
        }

        [TestMethod]
        public void TestProcessMove_fullGame_winPrimaryDiagonal()
        {
            /*
            x o o
            . x .
            . . x
             */

            GameState gameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            gameState = TestMove(gameState, 0, 0, 'X');
            gameState = TestMove(gameState, 2, 0, 'O');
            gameState = TestMove(gameState, 1, 1, 'X');
            gameState = TestMove(gameState, 1, 0, 'O');
            TestFinalMove(gameState, 2, 2, 'X', GameStatus.WIN);
        }

        [TestMethod]
        public void TestProcessMove_fullGame_winSecondaryDiagonal()
        {
            /*
            x x o
            . o .
            o . x
             */

            GameState gameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            gameState = TestMove(gameState, 0, 0, 'X');
            gameState = TestMove(gameState, 2, 0, 'O');
            gameState = TestMove(gameState, 1, 0, 'X');
            gameState = TestMove(gameState, 1, 1, 'O');
            gameState = TestMove(gameState, 2, 2, 'X');
            TestFinalMove(gameState, 0, 2, 'O', GameStatus.WIN);
        }

        [TestMethod]
        public void TestProcessMove_fullGame_tie()
        {
            GameState gameState = new(CreateCleanBoard(), new List<Player> { new(initiatingPlayerId, 'X'), new(joiningPlayerId, 'O') });
            gameState = TestMove(gameState, 0, 0, 'X');
            gameState = TestMove(gameState, 1, 1, 'O');
            gameState = TestMove(gameState, 2, 0, 'X');
            gameState = TestMove(gameState, 1, 0, 'O');
            gameState = TestMove(gameState, 1, 2, 'X');
            gameState = TestMove(gameState, 0, 1, 'O');
            gameState = TestMove(gameState, 2, 1, 'X');
            gameState = TestMove(gameState, 2, 2, 'O');
            TestFinalMove(gameState, 0, 2, 'X', GameStatus.TIE);
        }

        private static char[][] CreateCleanBoard()
        {
            char[][] board = new char[3][];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new char[3];
            }
            return board;
        }

        private static char[][] GetUpdateBoard(GameState gameState, Move move)
        {
            gameState.Board[move.Y][move.X] = move.GameCharacter;
            return gameState.Board;
        }

        private GameState TestMove(GameState initialGameState, int moveX, int moveY, char moveGameCharacter)
        {
            Move move = new(initialGameState.Id, moveX, moveY, moveGameCharacter);
            GameState actualGameState = pluginUnderTest.ProcessMove(move, initialGameState);
            GameState expectedGameState = new GameState.Cloner(initialGameState).With(GetUpdateBoard(initialGameState, move)).Clone();
            expectedGameState.AdvancePlayer();
            Assert.AreEqual(expectedGameState, actualGameState);
            return expectedGameState;
        }

        private void TestFinalMove(GameState initialGameState, int moveX, int moveY, char moveGameCharacter, GameStatus expectedGameStatus)
        {
            Move move = new(initialGameState.Id, moveX, moveY, moveGameCharacter);
            GameState actualGameState = pluginUnderTest.ProcessMove(move, initialGameState);
            GameState expectedGameState = new GameState.Cloner(initialGameState).With(GetUpdateBoard(initialGameState, move)).Clone();
            expectedGameState.GameStatus = expectedGameStatus;
            Assert.AreEqual(expectedGameState, actualGameState);
        }

    }
}

   
