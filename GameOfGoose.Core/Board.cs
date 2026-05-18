using System.Collections.Generic;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Core
{
    /// <summary>
    /// Represents the game board.
    /// </summary>
    public class Board
    {
        private readonly IReadOnlyDictionary<int, ISpace> _spaces;

        /// <summary>
        /// Initializes a new instance of <see cref="Board"/> with the specified space dictionary. 
        /// </summary>
        /// <param name="spaces">A read-only dictionary mapping space numbers to their corresponding ISpace implementations.</param>
        public Board(IReadOnlyDictionary<int, ISpace> spaces)
        {
            _spaces = spaces;
        }

        /// <summary>
        /// Returns the space at the specified board position.
        /// </summary>
        /// <param name="position">The board position to look up.</param>
        /// <returns>The ISpace implementation at the given position.</returns>
        public ISpace GetSpace(int position)
        {
            return _spaces[position];
        }
    }
}