using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Death class.
    /// </summary>
    public class DeathTests
    {
        private readonly Death _death;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a Death space and a piece instance for testing.
        /// </summary>
        public DeathTests()
        {
            _death = new Death(58, 0);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction moves the piece to the correct board position.
        /// </summary>
        [Fact]
        public void SpaceAction_MovesPieceToCorrectDestination()
        {
            _death.SpaceAction(_piece, 2);
            Assert.Equal(0, _piece.CurrentPosition);
        }
    }
}