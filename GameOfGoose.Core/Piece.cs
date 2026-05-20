namespace GameOfGoose.Core
{
    /// <summary>
    /// Represents a game piece on the board.
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// Gets the piece number.
        /// </summary>
        public int PieceNumber { get; }

        /// <summary>
        /// Gets or sets the current position on the board.
        /// </summary>
        public int CurrentPosition { get; set; }

        /// <summary>
        /// Gets or sets the number of turns to skip.
        /// </summary>
        public int SkipTurns { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the piece is moving forward. Defaults to true. 
        /// </summary>
        public bool IsMovingForward { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating wheter a piece is stuck.
        /// </summary>
        public bool IsStuck { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the piece has won. 
        /// </summary>
        public bool HasWon { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Piece"/>  with the specified piece number. 
        /// </summary>
        /// <param name="pieceNumber">The unique number identifying this piece.</param>
        public Piece(int pieceNumber)
        {
            PieceNumber = pieceNumber;
        }

        /// <summary>
        /// Moves the piece to the specified board position.
        /// </summary>
        /// <param name="position">The board position to move the piece to.</param>
        public void MoveTo(int position)
        {
            CurrentPosition = position;
        }
    }
}