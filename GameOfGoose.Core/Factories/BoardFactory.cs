using System.Collections.Generic;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Core.Factories
{
    /// <summary>
    /// Factory for creating the game board.
    /// </summary>
    public class BoardFactory
    {
        /// <summary>
        /// Creates and returns a fully configured game board with all 64 spaces (positions 0 through 63).
        /// </summary>
        /// <param name="pieces">The list of pieces used to initialize the Well space.</param>
        /// <returns>A fully configured Board instance with all special and regular spaces.</returns>
        public static Board CreateBoard(IReadOnlyList<Piece> pieces)
        {
            var spaces = new Dictionary<int, ISpace>();
            var gooseSpaces = new List<int> { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

            spaces.Add(6, new Bridge(6, 12));
            spaces.Add(19, new Inn(19, 1));
            spaces.Add(31, new Well(31, pieces));
            spaces.Add(42, new Maze(42, 39));
            spaces.Add(52, new Prison(52, 3));
            spaces.Add(58, new Death(58, 0));
            spaces.Add(63, new End(63));

            foreach (var position in gooseSpaces)
            {
                spaces.Add(position, new Goose(position));
            }

            for (int i = 0; i <= 63; i++)
            {
                if (!spaces.ContainsKey(i))
                {
                    spaces.Add(i, new Regular(i));
                }
            }

            return new Board(spaces);
        }
    }
}