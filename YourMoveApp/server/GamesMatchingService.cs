﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.model;
using YourMoveApp.server.api;

namespace YourMoveApp.server
{
    internal class GamesMatchingService : IGamesMatchingService
    {
        private readonly HashSet<String> _unmatchedGameIds = new();
        private readonly IGameStateRepository _gameStateRepository;

        public GamesMatchingService(IGameStateRepository gameStateRpository)
        {
            this._gameStateRepository = gameStateRpository;
        }

        public string CreateNewGame(string initiatingPlayerId)
        {
            Player initiatingPlayer = new(initiatingPlayerId, 'X');
            GameState gameState = new(GameStatus.UMATCHED, CreateCleanBoard(), new List<Player> { initiatingPlayer });
            String newGameId = _gameStateRepository.Save(gameState);
            _unmatchedGameIds.Add(newGameId);
            return newGameId;
        }

        public List<GameState> GetUnmatchedGames()
        {
            List<String> sortedUmatchedGameIds = GetSortedUmatchedGameIds();
            List<GameState> unmatchedGameStates = new();
            sortedUmatchedGameIds.ForEach(gameId => {
                GameState gameState = _gameStateRepository.Find(gameId);
                if (gameState != null)
                {
                    unmatchedGameStates.Add(gameState);
                }
            });
            return unmatchedGameStates;
        }

        public Action<object> GetAction()
        {
            return Callback;
        }



        public void Callback(object obj)
        {
            System.Console.WriteLine("This Is a Callback!!! -- " + obj.ToString());
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

        private List<String> GetSortedUmatchedGameIds()
        {
            List<String> sortedUmatchedGameIds = _unmatchedGameIds.ToList();
            sortedUmatchedGameIds.Sort();
            return sortedUmatchedGameIds;
        }
    }
}
