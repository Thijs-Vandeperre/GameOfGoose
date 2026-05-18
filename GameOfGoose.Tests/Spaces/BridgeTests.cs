using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Bridge class.
    /// </summary>
    public class BridgeTests
    {
        private readonly Bridge _bridge;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a bridge and a piece instance for testing.
        /// </summary>
        public BridgeTests()
        {
            _bridge = new Bridge(6, 12);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction moves the piece to the correct board position.
        /// </summary>
        [Fact]
        public void SpaceAction_MovesPieceToCorrectDestination()
        {
            _bridge.SpaceAction(_piece, 2);
            Assert.Equal(12, _piece.CurrentPosition);
        }
    }
}