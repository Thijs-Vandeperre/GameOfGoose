using GameOfGoose.Core;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Factories;
using System.Collections.Generic;
using Xunit;

namespace GameOfGoose.Tests.Engine.Rules
{
    /// <summary>
    /// Contains unit tests for the FirstTurnRule.
    /// </summary>
    public class FirstTurnRuleTests
    {
        private readonly FirstTurnRule _rule;

        public FirstTurnRuleTests()
        {
            _rule = new FirstTurnRule();
        }

        private GameContext CreateContext(int roll1, int roll2, int turnNumber = 1, int startPosition = 0)
        {
            var pieces = PieceFactory.CreatePieces(1);
            pieces[0].MoveTo(startPosition);
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);

            return new GameContext
            {
                Player = players[0],
                Board = board,
                Roll1 = roll1,
                Roll2 = roll2,
                TurnNumber = turnNumber
            };
        }

        /// <summary>
        /// Verifies that special first turn rolls move the piece to the correct position.
        /// </summary>
        [Theory]
        [InlineData(5, 4, 26)]
        [InlineData(4, 5, 26)]
        [InlineData(6, 3, 53)]
        [InlineData(3, 6, 53)]
        public void Apply_SpecialRollOnFirstTurn_MovesPieceCorrectly(int roll1, int roll2, int expected)
        {
            var context = CreateContext(roll1, roll2, turnNumber: 1);

            _rule.Apply(context);

            Assert.Equal(expected, context.Player.Piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that the rule does nothing when it is not the first turn.
        /// </summary>
        [Theory]
        [InlineData(5, 4)]
        [InlineData(4, 5)]
        [InlineData(6, 3)]
        [InlineData(3, 6)]
        public void Apply_SpecialRollNotFirstTurn_DoesNothing(int roll1, int roll2)
        {
            var context = CreateContext(roll1, roll2, turnNumber: 2);

            _rule.Apply(context);

            Assert.Equal(0, context.Player.Piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that a non-special roll on the first turn does not trigger the rule.
        /// </summary>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 3)]
        [InlineData(6, 6)]
        public void Apply_NonSpecialRollOnFirstTurn_DoesNothing(int roll1, int roll2)
        {
            var context = CreateContext(roll1, roll2, turnNumber: 1);

            _rule.Apply(context);

            Assert.Equal(0, context.Player.Piece.CurrentPosition);
        }
    }
}