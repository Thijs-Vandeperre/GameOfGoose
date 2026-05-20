using GameOfGoose.Core;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Factories;
using System.Collections.Generic;
using Xunit;

namespace GameOfGoose.Tests.Engine.Rules
{
    /// <summary>
    /// Contains unit tests for the BounceRule.
    /// </summary>
    public class BounceRuleTests
    {
        private BounceRule _rule;
        private GameContext CreateContext(int startPosition)
        {
            var piece = PieceFactory.CreatePieces(1)[0];
            piece.MoveTo(startPosition);

            var player = PlayerFactory.CreatePlayers(new List<Piece> { piece })[0];
            var board = BoardFactory.CreateBoard(new List<Piece> { piece });

            return new GameContext
            {
                Player = player,
                Board = board,
                Roll1 = 0,
                Roll2 = 0,
                TurnNumber = 1
            };
        }

        public BounceRuleTests()
        {
            _rule = new BounceRule();
        }

        /// <summary>
        /// Verifies that BounceRule correctly bounces a piece that overshoots the final position.
        /// </summary>
        [Theory]
        [InlineData(64, 62)]
        [InlineData(70, 56)]
        [InlineData(75, 51)]
        public void Apply_Overshoot_BouncesCorrectly(int startPosition, int expected)
        {
            var context = CreateContext(startPosition);

            _rule.Apply(context);

            Assert.Equal(expected, context.Player.Piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that BounceRule does nothing when piece is on or before the end position.
        /// </summary>
        [Theory]
        [InlineData(63)]
        [InlineData(50)]
        [InlineData(1)]
        public void Apply_NoOvershoot_DoesNotMove(int startPosition)
        {
            var context = CreateContext(startPosition);

            _rule.Apply(context);

            Assert.Equal(startPosition, context.Player.Piece.CurrentPosition);
        }
    }
}