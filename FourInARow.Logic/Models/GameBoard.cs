using System;
using System.Collections.Generic;
using System.Linq;

namespace FourInARow.Logic.Models
{
    /// <summary>
    /// Board that holds <see cref="Coin">Coin</see>'s
    /// Holds and executes the game rules
    /// </summary>
    public class GameBoard
    {
        private Coin[][] _board;
        private readonly int _colums;
        private readonly int _rows;
        private int _turnsPlayed;

        /// <summary>
        /// State of the game
        /// </summary>
        public GameState State { get; private set; }

        /// <summary>
        /// Create a new game board
        /// </summary>
        /// <param name="colums">Amount of colums in the board</param>
        /// <param name="rows">Amount of rows in a colum</param>
        public GameBoard(int colums, int rows)
        {
            _colums = colums;
            _rows = rows;
            Reset();
        }

        /// <summary>
        /// Get a coin on a single position in the board
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Coin GetCoinOnPosition(int x, int y)
        {
            return _board[x][y];
        }

        /// <summary>
        /// Reset the board and initialize a new game
        /// </summary>
        public void Reset()
        {
            _board = new Coin[_colums][];
            for (var i = 0; i < _board.Length; i++)
            {
                _board[i] = new Coin[_rows];
            }

            State = GameState.Playing;
        }

        /// <summary>
        /// Try to place a coin on the board, checks if the rules alow this coin to be placed, and executes win check logic
        /// </summary>
        /// <param name="owner">Player who played the coin</param>
        /// <param name="x">X coordinate of the coin</param>
        /// <param name="y">Y coordinate of the coin</param>
        /// <returns>A boolean if the coin has been placed or not</returns>
        public bool Place(Owner owner, int x, int y)
        {
            if (x >= _colums || x < 0)
                return false;
            if (y >= _rows || y < 0)
                return false;

            if (_board[x][y] != null)
                return false;

            
            //lets make sure y is the correct y, perhaps the coin needs to drop more, or move up
            for (var i = 0; i < _rows; i++)
            {
                var coin = GetCoinOnPosition(x, i);
                if (coin == null)
                    y = i;
            }

            _board[x][y] = new Coin(owner);
            _turnsPlayed++;
            UpdateState(x, y);
            return true;
        }

        //Reclaculate the state based on the last move
        private void UpdateState(int x, int y)
        {
            //if there are less then 7 turns played then there can be no winner just yey, so we skip the winning logic
            if(_turnsPlayed < 7)
                return;
            
            //Stone placed in top row, calculating if we have a tie is fastes way to the finish
            if (y == _rows - 1 && IsTie())
            {
                State = GameState.Tie;
                return;
            }

            var baseCoin = GetCoinOnPosition(x, y);

            //Check if any of the coins surrounding the new coin has neigbours from the same color, else continue the game
            var adjacentCoins = AdjacentCoins(x, y, baseCoin.Owner).ToList();
            if (!adjacentCoins.Any())
                return;

            var directions = GetCheckDirections(x, y, adjacentCoins);
            foreach (var direction in directions)
            {
                if (!IsWinner(x, y, baseCoin.Owner, direction))
                    continue;

                State = baseCoin.Owner == Owner.PlayerOne ?
                    GameState.PlayerOneWin :
                    GameState.PlayerTwoWin;
                break;
            }
        }

        private bool IsWinner(int x, int y, Owner owner, Direction searchDirection)
        {
            //we start at 1 since we just placed that one
            var coins = 1;

            switch (searchDirection)
            {
                case Direction.VerticalDown:
                case Direction.VerticalUp:
                    SearchVerticalCoins(x, y, owner, true, ref coins);
                    if (coins < 4)
                        SearchVerticalCoins(x, y, owner, false, ref coins);
                    break;
                case Direction.HorizontalLeft:
                case Direction.HorizontalRight:
                    SearchHorizontalCoins(x, y, owner, true, ref coins);
                    if (coins < 4)
                        SearchHorizontalCoins(x, y, owner, false, ref coins);
                    break;
                case Direction.DiagonalUpLeft:
                case Direction.DiagonalDownRight:
                    SearchDiagonalNegativeCoins(x, y, owner, true, ref coins);
                    if (coins < 4)
                        SearchDiagonalNegativeCoins(x, y, owner, false, ref coins);
                    break;
                case Direction.DiagonalUpRight:
                case Direction.DiagonalDownLeft:
                    SearchDiagonalPositiveCoins(x, y, owner, true, ref coins);
                    if (coins < 4)
                        SearchDiagonalPositiveCoins(x, y, owner, false, ref coins);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchDirection), searchDirection, null);
            }
            return coins >= 4;
        }

        private void SearchVerticalCoins(int x, int y, Owner owner, bool direction, ref int coins)
        {
            if (direction)
            {
                for (var i = x; i < x + 3; i++)
                {
                    if (i >= _rows)
                        break;

                    if (GetCoinOnPosition(i, y).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
            else
            {
                for (var i = x; i > x - 3; i++)
                {
                    if (i < 0)
                        break;

                    if (GetCoinOnPosition(i, y).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
        }

        private void SearchHorizontalCoins(int x, int y, Owner owner, bool direction, ref int coins)
        {
            if (direction)
            {
                for (var i = y; i < y + 3; i++)
                {
                    if (i >= _colums)
                        break;

                    if (GetCoinOnPosition(x, i).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
            else
            {
                for (var i = y; i > y - 3; i++)
                {
                    if (i < 0)
                        break;

                    if (GetCoinOnPosition(x, i).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
        }

        private void SearchDiagonalPositiveCoins(int x, int y, Owner owner, bool direction, ref int coins)
        {
            if (direction)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (x + i >= _rows || y + i >= _colums)
                        break;

                    if (GetCoinOnPosition(x + i, y + i).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
            else
            {
                for (var i = 0; i < 3; i++)
                {
                    if (x - i < 0 || y - i < 0)
                        break;

                    if (GetCoinOnPosition(x - i, y - i).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
        }

        private void SearchDiagonalNegativeCoins(int x, int y, Owner owner, bool direction, ref int coins)
        {
            if (direction)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (x + i >= _rows || y - i < 0)
                        break;

                    if (GetCoinOnPosition(x + i, y - i).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
            else
            {
                for (var i = 0; i < 3; i++)
                {
                    if (x - i < 0 || y + i >= _colums)
                        break;

                    if (GetCoinOnPosition(x - i, y = i).Owner == owner)
                        coins++;
                    else
                        break;
                }
            }
        }

        private bool IsTie()
        {
            var tie = true;
            var topRow = _rows - 1;
            for (var i = 0; i < _colums; i++)
            {
                if (_board[i][topRow] != null)
                    continue;

                tie = false;
                break;
            }
            return tie;
        }

        private IEnumerable<Tuple<int, int>> AdjacentCoins(int x, int y, Owner owner)
        {
            //search the 3x3 grid around the given the x,y from the coin 
            for (var c = x - 1; c <= x + 1; c++)
                for (var r = y - 1; r <= y + 1; r++)
                    if (r >= 0 && c >= 0 && c < _colums && r < _rows && !(r == y && c == x))
                        if (_board[c][r]?.Owner == owner)
                            yield return new Tuple<int, int>(c, r);
        }

        private static IEnumerable<Direction> GetCheckDirections(int x, int y, IEnumerable<Tuple<int, int>> adjecentCoins)
        {
            foreach (var adjecentCoin in adjecentCoins)
            {
                bool? left = null;
                bool? down = null;
                var diagonal = false;

                if (adjecentCoin.Item1 < x)
                    left = true;
                else if (adjecentCoin.Item1 > x)
                    left = false;
                if (adjecentCoin.Item2 < y)
                    down = true;
                else if (adjecentCoin.Item2 > y)
                    down = false;
                if (left.HasValue && down.HasValue)
                    diagonal = true;
                yield return GetDirectionFromVariables(left, down, diagonal);
            }
        }

        private static Direction GetDirectionFromVariables(bool? left, bool? down, bool diagonal)
        {
            // ReSharper disable PossibleInvalidOperationException if diagonal that means both down and left both are filled
            if (diagonal)
            {
                if (left.Value && down.Value)
                    return Direction.DiagonalDownLeft;
                if (!left.Value && down.Value)
                    return Direction.DiagonalDownRight;
                return left.Value ? Direction.DiagonalUpLeft : Direction.DiagonalUpRight;
            }
            // ReSharper restore PossibleInvalidOperationException

            if (left.HasValue && left.Value)
                return Direction.HorizontalLeft;
            if (left.HasValue)
                return Direction.HorizontalRight;

            if (down.HasValue && down.Value)
                return Direction.VerticalDown;

            return Direction.VerticalUp;
        }

        private enum Direction
        {
            VerticalDown,
            VerticalUp,
            HorizontalLeft,
            HorizontalRight,
            DiagonalUpLeft,
            DiagonalUpRight,
            DiagonalDownLeft,
            DiagonalDownRight,
        }
    }

    /// <summary>
    /// All the possible game states
    /// </summary>
    public enum GameState
    {
        Playing,
        PlayerOneWin,
        PlayerTwoWin,
        Tie
    }
}