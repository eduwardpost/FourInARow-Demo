using FourInARow.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourInARow.Tests.BoardTests
{
    [TestClass]
    public class TestBoardPlacingLogic
    {
        [TestMethod]
        public void TestCreation()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;

            //Act
            var gameBoard = new GameBoard(colums, rows);

            //Assert
            Assert.IsNotNull(gameBoard);
        }

        [TestMethod]
        public void TestPlacement()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;

            //Act
            var placed = gameBoard.Place(owner, 0, 6);

            //Assert
            Assert.IsTrue(placed);
        }

        [TestMethod]
        public void TestPlacementWithNegativeRow()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;

            //Act
            var placed = gameBoard.Place(owner, 0, -1);

            //Assert
            Assert.IsFalse(placed);
        }


        [TestMethod]
        public void TestPlacementWithNegativeColum()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;

            //Act
            var placed = gameBoard.Place(owner, -1, 6);

            //Assert
            Assert.IsFalse(placed);
        }

        [TestMethod]
        public void TestPlacementWithOutOfBoundRow()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;

            //Act
            var placed = gameBoard.Place(owner, 0, 7);

            //Assert
            Assert.IsFalse(placed);
        }


        [TestMethod]
        public void TestPlacementWithOutOfBoundColum()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;

            //Act
            var placed = gameBoard.Place(owner, 6, 6);

            //Assert
            Assert.IsFalse(placed);
        }

        [TestMethod]
        public void TestPlacemenDoublePosition()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;
            gameBoard.Place(owner, 0, 6);


            //Act
            var placed = gameBoard.Place(owner, 0, 6);

            //Assert
            Assert.IsFalse(placed);
        }
    }
}
