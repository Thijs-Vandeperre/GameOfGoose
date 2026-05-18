using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Goose class.
    /// </summary>
    public class GooseTests
    {
        private readonly Goose _goose;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a Regular space and a piece instance for testing.
        /// </summary>
        public GooseTests()
        {
            _goose = new Goose(5);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction moves the piece roll spaces forward.
        /// </summary>
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(11)]
        public void SpaceAction_MovesPieceRollSpacesForward(int roll)
        {
            _goose.SpaceAction(_piece, roll);
            Assert.Equal(5 + roll, _piece.CurrentPosition);
            
        }

        /// <summary>
        /// Verifies that SpaceAction moves the piece roll spaces backwards.
        /// </summary>
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(11)]
        public void SpaceAction_MovesPieceRollSpacesBackwards(int roll)
        {
            _piece.IsMovingForward = false;
            _goose.SpaceAction(_piece, roll);
            Assert.Equal(5 - roll, _piece.CurrentPosition);
            
        }
    }
}