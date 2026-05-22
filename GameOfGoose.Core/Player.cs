namespace GameOfGoose.Core
{
    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets the piece assigned to this player. 
        /// </summary>
        public Piece Piece { get; }

        /// <summary>
        /// Gets the player number.
        /// </summary>
        public int PlayerNumber { get; }

        /// <summary>
        /// Gets the player name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Player"/> with the specified piece and player number.
        /// </summary>
        /// <param name="piece">The piece assigned to this player.</param>
        /// <param name="playerNumber">The unique number identifying this player.</param>
        public Player(Piece piece, int playerNumber)
        {
            Piece = piece;
            PlayerNumber = playerNumber;
            Name = $"Player {playerNumber}";
        }
    }
}