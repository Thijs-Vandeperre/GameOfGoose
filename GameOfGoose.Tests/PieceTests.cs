using GameOfGoose.Core;

namespace GameOfGoose.Tests
{
    /// <summary>
    /// Contains unit tests for the Piece class.
    /// </summary>
    public class PieceTests
    {
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a Piece instance for testing.
        /// </summary>
        public PieceTests()
        {
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that the default values of a Piece are correct.
        /// </summary>
        [Fact]
        public void Piece_HasCorrectDefaultValues()
        {
            Assert.Equal(0, _piece.CurrentPosition);
            Assert.Equal(0, _piece.SkipTurns);
            Assert.True(_piece.IsMovingForward);
            Assert.False(_piece.HasWon);
        }

        /// <summary>
        /// Verifies that MoveTo updates the current position.
        /// </summary>
        /// <param name="position">The position the piece should move to.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(32)]
        [InlineData(63)]
        public void MoveTo_UpdatesCurrentPosition(int position)
        {
            _piece.MoveTo(position);
            Assert.Equal(position, _piece.CurrentPosition);
        }
    }
}