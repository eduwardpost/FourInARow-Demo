using System;
using FourInARow.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourInARow.Tests.BoardTests
{
    [TestClass]
    public class TestBoardWinningLogic
    {
        private GameBoard _gameBoard;
        private int _colums;
        private int _rows;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            _colums = 6;
            _rows = 7;
            _gameBoard = new GameBoard(_colums, _rows);
        }

        [TestMethod]
        public void TestHorizontalLeftToRightWin()
        {
            //Arrange
            _gameBoard.Place(Owner.PlayerOne, 0, 6);
            _gameBoard.Place(Owner.PlayerOne, 1, 6);
            _gameBoard.Place(Owner.PlayerOne, 2, 6);

            _gameBoard.Place(Owner.PlayerTwo, 5, 0);
            _gameBoard.Place(Owner.PlayerTwo, 5, 1);
            _gameBoard.Place(Owner.PlayerTwo, 5, 2);

            //Act
            _gameBoard.Place(Owner.PlayerOne, 3, 6);

            //Assert
            Console.WriteLine(_gameBoard);
            Assert.AreEqual(GameState.PlayerOneWin, _gameBoard.State);
        }

        [TestMethod]
        public void TestHorizontalRightToLeftWin()
        {
            //Arrange
            _gameBoard.Place(Owner.PlayerOne, 3, 6);
            _gameBoard.Place(Owner.PlayerOne, 2, 6);
            _gameBoard.Place(Owner.PlayerOne, 1, 6);

            _gameBoard.Place(Owner.PlayerTwo, 5, 0);
            _gameBoard.Place(Owner.PlayerTwo, 5, 1);
            _gameBoard.Place(Owner.PlayerTwo, 5, 2);

            //Act
            _gameBoard.Place(Owner.PlayerOne, 0, 6);

            //Assert
            Console.WriteLine(_gameBoard);
            Assert.AreEqual(GameState.PlayerOneWin, _gameBoard.State);
        }

        [TestMethod]
        public void TestHorizontalInbetweenWin()
        {
            //Arrange
            _gameBoard.Place(Owner.PlayerOne, 3, 6);
            _gameBoard.Place(Owner.PlayerOne, 2, 6);
            _gameBoard.Place(Owner.PlayerOne, 0, 6);

            _gameBoard.Place(Owner.PlayerTwo, 5, 0);
            _gameBoard.Place(Owner.PlayerTwo, 5, 1);
            _gameBoard.Place(Owner.PlayerTwo, 5, 2);

            //Act
            _gameBoard.Place(Owner.PlayerOne, 1, 6);

            //Assert
            Console.WriteLine(_gameBoard);
            Assert.AreEqual(GameState.PlayerOneWin, _gameBoard.State);
        }

        [TestMethod]
        public void TestVerticalUpWin()
        {
            //Arrange
            _gameBoard.Place(Owner.PlayerOne, 0, 0);
            _gameBoard.Place(Owner.PlayerOne, 0, 1);
            _gameBoard.Place(Owner.PlayerOne, 0, 2);

            _gameBoard.Place(Owner.PlayerTwo, 5, 0);
            _gameBoard.Place(Owner.PlayerTwo, 5, 1);
            _gameBoard.Place(Owner.PlayerTwo, 5, 2);

            //Act
            _gameBoard.Place(Owner.PlayerOne, 0, 3);

            //Assert
            Console.WriteLine(_gameBoard);
            Assert.AreEqual(GameState.PlayerOneWin, _gameBoard.State);
        }

        [TestMethod]
        public void TestDiagonalUpRightWin()
        {
            //Arrange
            _gameBoard.Place(Owner.PlayerOne, 0, 0);
            _gameBoard.Place(Owner.PlayerTwo, 1, 0);

            _gameBoard.Place(Owner.PlayerOne, 1, 1);
            _gameBoard.Place(Owner.PlayerTwo, 2, 0);

            _gameBoard.Place(Owner.PlayerOne, 3, 0);
            _gameBoard.Place(Owner.PlayerTwo, 2, 1);

            _gameBoard.Place(Owner.PlayerOne, 2, 2);
            _gameBoard.Place(Owner.PlayerTwo, 3, 1);

            _gameBoard.Place(Owner.PlayerOne, 3, 2);
            _gameBoard.Place(Owner.PlayerTwo, 4, 0);

            //Act
            _gameBoard.Place(Owner.PlayerOne, 3, 3);

            //Assert
            Console.WriteLine(_gameBoard);
            Assert.AreEqual(GameState.PlayerOneWin, _gameBoard.State);
        }

    }
}
