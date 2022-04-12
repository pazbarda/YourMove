﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class GameState
    {
        private int Id { get; }
        private GameStatus GameStatus { get; }
        
        char[][] Board {
            get { return this._board; } 
        }
        Player[] Players { get; }
        Player NextPlayer
        {
            get { return Players[_nextPlayerIndex]; }
        }

        private char[][] _board;
        private int _nextPlayerIndex;


        public GameState(int id, GameStatus gameStatus, char[][] board, Player[] players) 
            : this(id, gameStatus, board, players, 0)
        {
        }

        private GameState(int id, GameStatus gameStatus, char[][] board, Player[] players, int nextPlayerIndex)
        {
            this.Id = id;
            this.GameStatus = gameStatus;
            this._board = board;
            this.Players = players;
            this._nextPlayerIndex = nextPlayerIndex;
        }

        public void AdvancePlayer()
        {
            if (_nextPlayerIndex == Players.Length - 1)
            {
                _nextPlayerIndex = 0;
            } else
            {
                _nextPlayerIndex++;
            }
        }

        public class Cloner
        {
            // private readonly GameState _gameState;

            private readonly int _id;
            private readonly GameStatus _gameStatus;
            private char[][] _board;
            private readonly Player[] _players;
            private int _nextPlayerIndex;

            public Cloner(GameState gameState)
            {
                this._id = gameState.Id;
                this._gameStatus = gameState.GameStatus;
                this._board = gameState.Board;
                this._players = new Player[gameState.Players.Length];
                Array.Copy(gameState.Players, this._players, gameState.Players.Length);
                this._nextPlayerIndex = gameState._nextPlayerIndex;
            }

            public Cloner With(char[][] board)
            {
                this._board = board;
                return this;
            }

            public GameState Clone()
            {
                return new GameState(
                        this._id,
                        this._gameStatus,
                        this._board,
                        this._players,
                        this._nextPlayerIndex
                    );
            }
        }
        
    }
}