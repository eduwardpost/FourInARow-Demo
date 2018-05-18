using FourInARow.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourInARow.Tests.BoardTests
{
    [TestClass]
    public class TestBoardViewLogic
    {
        [TestMethod]
        public void TestGetCoinOnPosition()
        {
            //Arrange
            const int colums = 6;
            const int rows = 7;
            var gameBoard = new GameBoard(colums, rows);
            const Owner owner = Owner.PlayerOne;
            gameBoard.Place(owner, 0, 6);
            //Act
            var coin = gameBoard.GetCoinOnPosition(0, 6);

            //Assert
            Assert.IsNotNull(coin);
            Assert.AreEqual(Owner.PlayerOne, coin.Owner);
        }
    }
}
