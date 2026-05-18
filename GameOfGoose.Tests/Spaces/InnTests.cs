using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Inn class.
    /// </summary>
    public class InnTests
    {
        private readonly Inn _inn;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up an Inn space and a piece instance for testing.
        /// </summary>
        public InnTests()
        {
            _inn = new Inn(19, 1);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction sets the SkipTurns property of the piece to the correct value.
        /// </summary>
        [Fact]
        public void SpaceAction_SetsPiecesSkipTurnsToCorrectValue()
        {
            _inn.SpaceAction(_piece, 2);
            Assert.Equal(1, _piece.SkipTurns);
        }
    }
}