using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the End class.
    /// </summary>
    public class EndTests
    {
        private readonly End _end;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up an End space and a piece instance for testing.
        /// </summary>
        public EndTests()
        {
            _end = new End(63);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction sets the HasWon property of the piece to the correct value.
        /// </summary>
        [Fact]
        public void SpaceAction_SetsPiecesHasWonToCorrectValue()
        {
            _end.SpaceAction(_piece, 2);
            Assert.True(_piece.HasWon);
        }
    }
}