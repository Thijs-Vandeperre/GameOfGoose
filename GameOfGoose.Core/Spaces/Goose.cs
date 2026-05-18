namespace GameOfGoose.Core.Spaces
{
    /// <summary>
    /// Represents the Goose space, implements the ISpace interface.
    /// </summary>
    public class Goose : ISpace
    {
        /// <summary>
        /// Gets the space number.
        /// </summary>
        public int SpaceNumber { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Goose"/> with the specified space number.
        /// </summary>
        /// <param name="spaceNumber">The unique number identifying this space.</param>
        public Goose(int spaceNumber)
        {
            SpaceNumber = spaceNumber;
        }

        /// <summary>
        /// Performs the action corresponding to this space when a piece lands on it. 
        /// </summary>
        /// <param name="piece">The piece that landed on this space.</param>
        /// <param name="roll">The total value of the dice roll.</param>
        public void SpaceAction(Piece piece, int roll)
        {
            int direction = piece.IsMovingForward ? 1 : -1;
            piece.MoveTo(SpaceNumber + (roll * direction));
        }
    }
}