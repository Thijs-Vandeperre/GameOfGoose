using GameOfGoose.Core;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Factories;

namespace GameOfGoose.Tests.Engine.Rules
{
    /// <summary>
    /// Contains unit tests for the SpaceActionRule.
    /// </summary>
    public class SpaceActionRuleTests
    {
        private readonly SpaceActionRule _rule;

        public SpaceActionRuleTests()
        {
            _rule = new SpaceActionRule();
        }

        private GameContext CreateContext(int startPosition, int roll1 = 1, int roll2 = 1)
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
                TurnNumber = 2
            };
        }

        #region Regular

        /// <summary>
        /// Verifies that landing on a regular space does nothing.
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        public void Apply_RegularSpace_DoesNotMove(int startPosition)
        {
            var context = CreateContext(startPosition);

            _rule.Apply(context);

            Assert.Equal(startPosition, context.Player.Piece.CurrentPosition);
        }

        #endregion

        #region Bridge

        /// <summary>
        /// Verifies that landing on the Bridge moves the piece to space 12.
        /// </summary>
        [Fact]
        public void Apply_Bridge_MovesToDestination()
        {
            var context = CreateContext(6);

            _rule.Apply(context);

            Assert.Equal(12, context.Player.Piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that landing on the Bridge does not apply space 12's action (Bridge -> Regular 12 = no extra movement).
        /// </summary>
        [Fact]
        public void Apply_Bridge_DoesNotChainFurtherAction()
        {
            // Space 12 is Regular, so final position should stay at 12 with no further movement.
            var context = CreateContext(6);

            _rule.Apply(context);

            Assert.Equal(12, context.Player.Piece.CurrentPosition);
            Assert.False(context.Player.Piece.HasWon);
        }

        #endregion

        #region Inn

        /// <summary>
        /// Verifies that landing on the Inn sets SkipTurns to 1.
        /// </summary>
        [Fact]
        public void Apply_Inn_SetsSkipTurns()
        {
            var context = CreateContext(19);

            _rule.Apply(context);

            Assert.Equal(1, context.Player.Piece.SkipTurns);
        }

        #endregion

        #region Well

        /// <summary>
        /// Verifies that landing on the Well sets IsStuck to true.
        /// </summary>
        [Fact]
        public void Apply_Well_SetsIsStuck()
        {
            var context = CreateContext(31);

            _rule.Apply(context);

            Assert.True(context.Player.Piece.IsStuck);
        }

        /// <summary>
        /// Verifies that a second piece landing on the Well frees the first piece.
        /// </summary>
        [Fact]
        public void Apply_Well_SecondPieceFreesFirst()
        {
            var pieces = PieceFactory.CreatePieces(2);
            pieces[0].MoveTo(31);
            pieces[0].IsStuck = true;
            pieces[1].MoveTo(31);
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);

            var context = new GameContext
            {
                Player = players[1],
                Board = board,
                Roll1 = 1,
                Roll2 = 1,
                TurnNumber = 2
            };

            _rule.Apply(context);

            Assert.False(pieces[0].IsStuck);
            Assert.True(pieces[1].IsStuck);
        }

        #endregion

        #region Maze

        /// <summary>
        /// Verifies that landing on the Maze moves the piece to space 39.
        /// </summary>
        [Fact]
        public void Apply_Maze_MovesToDestination()
        {
            var context = CreateContext(42);

            _rule.Apply(context);

            Assert.Equal(39, context.Player.Piece.CurrentPosition);
        }

        #endregion

        #region Prison

        /// <summary>
        /// Verifies that landing on the Prison sets SkipTurns to 3.
        /// </summary>
        [Fact]
        public void Apply_Prison_SetsSkipTurns()
        {
            var context = CreateContext(52);

            _rule.Apply(context);

            Assert.Equal(3, context.Player.Piece.SkipTurns);
        }

        #endregion

        #region Death

        /// <summary>
        /// Verifies that landing on Death moves the piece back to start.
        /// </summary>
        [Fact]
        public void Apply_Death_MovesToStart()
        {
            var context = CreateContext(58);

            _rule.Apply(context);

            Assert.Equal(0, context.Player.Piece.CurrentPosition);
        }

        #endregion

        #region Goose

        /// <summary>
        /// Verifies that landing on a goose space chains movement correctly.
        /// </summary>
        [Theory]
        [InlineData(5, 2, 3, 10)]   // 5 + 5 = 10, not a goose, stops
        [InlineData(9, 3, 3, 15)]   // 9 + 6 = 15, not a goose, stops
        [InlineData(5, 2, 2, 13)]   // 5 + 4 = 9 (goose) -> 9 + 4 = 13, stops
        [InlineData(9, 4, 5, 63)]   // 9 + 9 = 18 (goose) -> 27 -> 36 -> 45 -> 54 -> 63, stops
        [InlineData(59, 2, 3, 62)]  // 59 + 5 = 64 (goose, overshoots) -> bounces to 62, stops
        public void Apply_GooseSpace_ChainsMovementCorrectly(int startPosition, int roll1, int roll2, int expected)
        {
            var context = CreateContext(startPosition, roll1, roll2);

            _rule.Apply(context);

            Assert.Equal(expected, context.Player.Piece.CurrentPosition);
        }

        #endregion

        #region End

        /// <summary>
        /// Verifies that landing exactly on space 63 sets HasWon.
        /// </summary>
        [Fact]
        public void Apply_End_SetsHasWon()
        {
            var context = CreateContext(63);

            _rule.Apply(context);

            Assert.True(context.Player.Piece.HasWon);
        }

        /// <summary>
        /// Verifies that a goose chain landing on End sets HasWon.
        /// </summary>
        [Fact]
        public void Apply_GooseChainLandsOnEnd_SetsHasWon()
        {
            // Space 54 is a Goose. Roll 9: 54 + 9 = 63 (End).
            var context = CreateContext(54, 4, 5);

            _rule.Apply(context);

            Assert.True(context.Player.Piece.HasWon);
        }

        #endregion
    }
}