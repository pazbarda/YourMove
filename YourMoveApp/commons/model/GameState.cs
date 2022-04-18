using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.util;

namespace YourMoveApp.commons.model
{
    public class GameState
    {
        public String Id { get; set; }
        public GameStatus GameStatus { get; set; }

        public char[][] Board {
            get { return this._board; } 
        }
        public List<Player> Players { get; }
        public Player NextPlayer
        {
            get { return Players[_nextPlayerIndex]; }
        }

        private char[][] _board;
        private int _nextPlayerIndex;

        private GameState(String id, GameStatus gameStatus, char[][] board, List<Player> players, int nextPlayerIndex)
        {
            this.Id = id;
            this.GameStatus = gameStatus;
            this._board = board;
            this.Players = players;
            this._nextPlayerIndex = nextPlayerIndex;
        }

        public GameState(GameStatus gameStatus, char[][] board, List<Player> players) 
            : this(Guid.NewGuid().ToString("N"), gameStatus, board, players, 0)
        {
        }

        public void AdvancePlayer()
        {
            if (_nextPlayerIndex == Players.Count - 1)
            {
                _nextPlayerIndex = 0;
            } else
            {
                _nextPlayerIndex++;
            }
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public class Cloner
        {
            // private readonly GameState _gameState;

            private readonly String _id;
            private readonly GameStatus _gameStatus;
            private char[][] _board;
            private readonly List<Player> _players;
            private readonly int _nextPlayerIndex;

            public Cloner(GameState gameState)
            {
                this._id = gameState.Id;
                this._gameStatus = gameState.GameStatus;
                this._board = gameState.Board;
                this._players = ObjectUtil.CloneList(gameState.Players);
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
