using System.Collections.Generic;
using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

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
        /// Sets up a Well space, a pieces list, and a piece instance for testing.
        /// </summary>
        public WellTests()
        {
            _pieces = PieceFactory.CreatePieces(4);
            _well = new Well(31, _pieces);
            _thisPiece = _pieces[0];
            _otherPiece = _pieces[1];
        }

        /// <summary>
        /// Verifies that SpaceAction sets SkipTurns to one.
        /// </summary>
        [Fact]
        public void SpaceAction_AlwaysSetsSkipTurnsToOne()
        {
            _well.SpaceAction(_thisPiece, 2);
            Assert.Equal(1, _thisPiece.SkipTurns);
            
        }

        /// <summary>
        /// Verifies that SpaceAction frees a piece that is already in the well.
        /// </summary>
        [Fact]
        public void SpaceAction_FreesOtherPieceWhenAlreadyAtWell()
        {
            _otherPiece.MoveTo(31);
            _otherPiece.SkipTurns = 1;
            _well.SpaceAction(_thisPiece, 2);
            Assert.Equal(1, _thisPiece.SkipTurns);
            Assert.Equal(0, _otherPiece.SkipTurns);
        }
    }
}