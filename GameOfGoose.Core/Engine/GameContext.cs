using System.Collections.Generic;
using GameOfGoose.Core.Dice;

namespace GameOfGoose.Core.Engine
{
    /// <summary>
    /// Holds the state of a single turn for rule processing.
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Gets the current player.
        /// </summary>
        public Player Player { get; init; }

        /// <summary>
        /// Gets the game board.
        /// </summary>
        public Board Board { get; init; }

        /// <summary>
        /// Gets the first dice roll.
        /// </summary>
        public int Roll1 { get; init; }

        /// <summary>
        /// Gets the second dice roll.
        /// </summary>
        public int Roll2 { get; init; }

        /// <summary>
        /// Gets the current turn number.
        /// </summary>
        public int TurnNumber { get; init; }

        /// <summary>
        /// Gets the total roll value.
        /// </summary>
        public int TotalRoll => Roll1 + Roll2;
    }
}