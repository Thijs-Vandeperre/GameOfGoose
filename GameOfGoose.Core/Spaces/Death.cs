namespace GameOfGoose.Core.Spaces
{
    /// <summary>
    /// Represents the Death space, implements the ISpace interface.
    /// </summary>
    public class Death : ISpace
    {
        private readonly int _destination;

        /// <summary>
        /// Gets the space number.
        /// </summary>
        public int SpaceNumber { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Death"/> with the specified space number and destination.
        /// </summary>
        /// <param name="spaceNumber">The unique number identifying this space.</param>
        /// <param name="destination">The destination that the piece landing on this space moves to.</param>
        public Death(int spaceNumber, int destination)
        {
            SpaceNumber = spaceNumber;
            _destination = destination;
        }

        /// <summary>
        /// Performs the action corresponding to this space when a piece lands on it. 
        /// </summary>
        /// <param name="piece">The piece that landed on this space.</param>
        /// <param name="roll">The total value of the dice roll.</param>
        public void SpaceAction(Piece piece, int roll)
        {
            piece.MoveTo(_destination);
        }
    }
}