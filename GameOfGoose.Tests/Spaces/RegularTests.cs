using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Regular class.
    /// </summary>
    public class RegularTests
    {
        private readonly Regular _regular;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a Regular space and a piece instance for testing.
        /// </summary>
        public RegularTests()
        {
            _regular = new Regular(1);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction does not change the piece.
        /// </summary>
        [Fact]
        public void SpaceAction_DoesNotChangePiece()
        {
            _regular.SpaceAction(_piece, 2);
            Assert.Equal(0, _piece.CurrentPosition);
            Assert.False(_piece.HasWon);
            Assert.True(_piece.IsMovingForward);
            Assert.Equal(1, _piece.PieceNumber);
            Assert.Equal(0, _piece.SkipTurns);
        }
    }
}