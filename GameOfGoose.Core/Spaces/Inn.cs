namespace GameOfGoose.Core.Spaces
{
    /// <summary>
    /// Represents the Inn space, implements the ISpace interface.
    /// </summary>
    public class Inn : ISpace
    {
        private readonly int _skipTurns;
        
        /// <summary>
        /// Gets the space number.
        /// </summary>
        public int SpaceNumber { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Inn"/> with the specified space number and skip turns.
        /// </summary>
        /// <param name="spaceNumber">The unique number identifying this space.</param>
        /// <param name="skipTurns">The number of turns to skip when a piece lands on this space.</param>
        public Inn(int spaceNumber, int skipTurns)
        {
            SpaceNumber = spaceNumber;
            _skipTurns = skipTurns;
        }

        /// <summary>
        /// Performs the action corresponding to this space when a piece lands on it. 
        /// </summary>
        /// <param name="piece">The piece that landed on this space.</param>
        /// <param name="roll">The total value of the dice roll.</param>
        public void SpaceAction(Piece piece, int roll)
        {
            piece.SkipTurns = _skipTurns;
        }
    }
}