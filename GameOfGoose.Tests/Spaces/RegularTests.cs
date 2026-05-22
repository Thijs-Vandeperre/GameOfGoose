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
            _piece.MoveTo(10);
        }

        /// <summary>
        /// Verifies that SpaceAction does not change the piece's position.
        /// </summary>
        [Fact]
        public void SpaceAction_DoesNotChangePosition()
        {
            _regular.SpaceAction(_piece, 2);
            Assert.Equal(10, _piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that SpaceAction does not set HasWon.
        /// </summary>
        [Fact]
        public void SpaceAction_DoesNotSetHasWon()
        {
            _regular.SpaceAction(_piece, 2);
            Assert.False(_piece.HasWon);
        }

        /// <summary>
        /// Verifies that SpaceAction does not change IsMovingForward.
        /// </summary>
        [Fact]
        public void SpaceAction_DoesNotChangeIsMovingForward()
        {
            _regular.SpaceAction(_piece, 2);
            Assert.True(_piece.IsMovingForward);
        }

        /// <summary>
        /// Verifies that SpaceAction does not set SkipTurns.
        /// </summary>
        [Fact]
        public void SpaceAction_DoesNotSetSkipTurns()
        {
            _regular.SpaceAction(_piece, 2);
            Assert.Equal(0, _piece.SkipTurns);
        }
    }
}