namespace GameOfGoose.Core.Spaces
{
    /// <summary>
    /// Represents a single space on the game board. 
    /// </summary>
    public interface ISpace
    {
        /// <summary>
        /// Gets the space number.
        /// </summary>
        public int SpaceNumber { get; }

        /// <summary>
        /// Performs the action corresponding to this space when a piece lands on it. 
        /// </summary>
        /// <param name="piece">The piece that landed on this space.</param>
        /// <param name="roll">The total value of the dice roll.</param>
        public void SpaceAction(Piece piece, int roll);
    }
}