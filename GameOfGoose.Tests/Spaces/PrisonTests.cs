using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Prison class.
    /// </summary>
    public class PrisonTests
    {
        private readonly Prison _prison;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a Prison space and a piece instance for testing.
        /// </summary>
        public PrisonTests()
        {
            _prison = new Prison(52, 3);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction sets the SkipTurns property of the piece to the correct value.
        /// </summary>
        [Fact]
        public void SpaceAction_SetsPiecesSkipTurnsToCorrectValue()
        {
            _prison.SpaceAction(_piece, 2);
            Assert.Equal(3, _piece.SkipTurns);
        }
    }
}