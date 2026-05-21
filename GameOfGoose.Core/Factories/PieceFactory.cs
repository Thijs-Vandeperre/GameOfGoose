using System.Collections.Generic;

namespace GameOfGoose.Core.Factories
{
    /// <summary>
    /// Factory for creating game pieces.
    /// </summary>
    public class PieceFactory
    {
        /// <summary>
        /// Creates the specified number of pieces.
        /// </summary>
        /// <param name="numberOfPieces">The number of pieces to create.</param>
        /// <returns>A list of Piece instances numbered 1 through numberOfPieces.</returns>
        public static IReadOnlyList<Piece> CreatePieces(int numberOfPieces)
        {
            var pieces = new List<Piece>();
            for (int i = 0; i < numberOfPieces; i++)
            {
                var piece = new Piece(i + 1);
                pieces.Add(piece);
            }
            return pieces;
        }
    }
}