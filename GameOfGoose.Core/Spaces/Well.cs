using System.Collections.Generic;

namespace GameOfGoose.Core.Spaces
{
    /// <summary>
    /// Represents the Well space, implements the ISpace interface.
    /// </summary>
    public class Well : ISpace
    {
        private readonly IReadOnlyList<Piece> _pieces;
        /// <summary>
        /// Gets the space number.
        /// </summary>
        public int SpaceNumber { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Well"/> with the specified space number and pieces list.
        /// </summary>
        /// <param name="spaceNumber">The unique number identifying this space.</param>
        /// <param name="pieces">The list of pieces on the game board.</param>
        public Well(int spaceNumber, IReadOnlyList<Piece> pieces)
        {
            SpaceNumber = spaceNumber;
            _pieces = pieces;
        }

        /// <summary>
        /// Performs the action corresponding to this space when a piece lands on it. 
        /// </summary>
        /// <param name="thisPiece">The piece that landed on this space.</param>
        /// <param name="roll">The total value of the dice roll.</param>
        public void SpaceAction(Piece thisPiece, int roll)
        {
            thisPiece.SkipTurns = 1;
            foreach (Piece piece in _pieces)
            {
                if (piece.CurrentPosition == SpaceNumber && piece != thisPiece)
                {
                    piece.SkipTurns = 0;
                }
            }
        }
    }
}