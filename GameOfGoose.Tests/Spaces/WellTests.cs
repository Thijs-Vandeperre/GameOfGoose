using System.Collections.Generic;
using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;
using GameOfGoose.Core.Factories;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Well class.
    /// </summary>
    public class WellTests
    {
        private readonly IReadOnlyList<Piece> _pieces;
        private readonly Well _well;
        private readonly Piece _thisPiece;
        private readonly Piece _otherPiece;

        /// <summary>
        /// Sets up a Well space, a pieces list, and piece instances for testing.
        /// </summary>
        public WellTests()
        {
            _pieces = PieceFactory.CreatePieces(4);
            _well = new Well(31, _pieces);
            _thisPiece = _pieces[0];
            _otherPiece = _pieces[1];
        }

        /// <summary>
        /// Verifies that SpaceAction sets IsStuck to true.
        /// </summary>
        [Fact]
        public void SpaceAction_SetsIsStuckToTrue()
        {
            _well.SpaceAction(_thisPiece, 2);
            Assert.True(_thisPiece.IsStuck);
        }

        /// <summary>
        /// Verifies that SpaceAction frees a piece that is already in the well.
        /// </summary>
        [Fact]
        public void SpaceAction_FreesOtherPieceWhenAlreadyAtWell()
        {
            _otherPiece.MoveTo(31);
            _otherPiece.IsStuck = true;

            _well.SpaceAction(_thisPiece, 2);

            Assert.True(_thisPiece.IsStuck);
            Assert.False(_otherPiece.IsStuck);
        }

        /// <summary>
        /// Verifies that when a third piece arrives while two pieces are already stuck,
        /// all previously stuck pieces at the well are freed, and only the arriving piece becomes stuck.
        /// </summary>
        [Fact]
        public void SpaceAction_ThirdPieceArrives_FreesFirstStuckPiece()
        {
            var firstStuck = _pieces[0];
            var secondStuck = _pieces[1];
            var arriving = _pieces[2];

            firstStuck.MoveTo(31);
            firstStuck.IsStuck = true;
            secondStuck.MoveTo(31);
            secondStuck.IsStuck = true;

            _well.SpaceAction(arriving, 2);

            Assert.False(firstStuck.IsStuck);
            Assert.False(secondStuck.IsStuck);
            Assert.True(arriving.IsStuck);
        }

        /// <summary>
        /// Verifies that a piece not at the well is not affected when another piece arrives.
        /// </summary>
        [Fact]
        public void SpaceAction_PieceElsewhere_IsNotAffected()
        {
            var elsewhere = _pieces[1];
            elsewhere.MoveTo(10);
            elsewhere.IsStuck = false;

            _well.SpaceAction(_thisPiece, 2);

            Assert.False(elsewhere.IsStuck);
        }
    }
}