using GameOfGoose.Core;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Factories;
using GameOfGoose.Core.Logging;
using System.Collections.Generic;

namespace GameOfGoose.Tests.Engine
{
    /// <summary>
    /// Contains unit tests for the Game class.
    /// </summary>
    public class GameTests
    {
        private readonly Game _game;
        private readonly IReadOnlyList<Player> _players;
        private readonly Board _board;

        private class NoOpInputReader : IInputReader
        {
            public void WaitForEnter() { }
        }

        private class NoOpLogger : ILogger
        {
            public void Log(string message) { }
        }

        public GameTests()
        {
            var pieces = PieceFactory.CreatePieces(4);
            _players = PlayerFactory.CreatePlayers(pieces);
            _board = BoardFactory.CreateBoard(pieces);

            var rules = new List<IGameRule>
            {
                new FirstTurnRule(),
                new BounceRule(),
                new SpaceActionRule()
            };

            _game = new Game(_players, _board, new TwoDiceRoll(new Die()), new NoOpLogger(), new GameFormatter(), new NoOpInputReader(), rules);
        }

        #region SkipTurns

        /// <summary>
        /// Verifies that skip turns decrement correctly.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_DecrementsValue()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 2;

            _game.NextTurn(player);

            Assert.Equal(1, player.Piece.SkipTurns);
        }

        /// <summary>
        /// Verifies that a skip turn prevents movement.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_DoesNotMove()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 1;
            player.Piece.MoveTo(10);

            _game.NextTurn(player);

            Assert.Equal(10, player.Piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies skip turn format output.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_ReturnsCorrectFormat()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 1;
            player.Piece.MoveTo(10);

            var result = _game.NextTurn(player);

            Assert.Equal("/ :S10", result);
        }

        #endregion
    }
}