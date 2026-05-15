using System.Collections.Generic;

namespace GameOfGoose.Core
{
    /// <summary>
    /// Factory for creating game players.
    /// </summary>
    public class PlayerFactory
    {
        /// <summary>
        /// Creates a list of players from the specified pieces, numbered starting from 1. 
        /// </summary>
        /// <param name="pieces">The list of pieces to assign to the players.</param>
        /// <returns>A list of Player instances numbered 1 through the number of pieces." </returns>
        public static List<Player> CreatePlayers(List<Piece> pieces)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < pieces.Count; i++)
            {
                Player player = new Player(pieces[i], i + 1);
                players.Add(player);
            }
            return players;
        }
    }
}