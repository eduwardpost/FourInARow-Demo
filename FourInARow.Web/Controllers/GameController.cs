using FourInARow.Logic.Ai;
using FourInARow.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace FourInARow.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private static GameBoard _gameBoard;
        private static bool _aiEnabled;

        [HttpGet("[action]/{columns}/{rows}/{ai}")]
        public IActionResult StartGame(int columns, int rows, bool ai)
        {
            _gameBoard = new GameBoard(columns, rows);
            _aiEnabled = ai;
            return Ok();
        }

        [HttpGet("[action]/{column}/{row}/{player}")]
        public IActionResult PlayMove(int column, int row, int player)
        {
            var owner = player == 1 ? Owner.PlayerOne : Owner.PlayerTwo;
            var moveSuccess = _gameBoard.Place(owner, column, row);
            if (moveSuccess && _aiEnabled)
            {
                var aiMove = Jarvis.PlayMove(_gameBoard);
                return Ok(aiMove);
            }
            if (moveSuccess)
            {
                Ok();
            }
            return BadRequest("Move non succses");
        }

        [HttpGet("[action]")]
        public IActionResult GameState()
        {
            return Ok(_gameBoard.State);
        }
    }
}
