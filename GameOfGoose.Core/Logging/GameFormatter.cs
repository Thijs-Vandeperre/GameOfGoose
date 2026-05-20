using System.Collections.Generic;
using System.Linq;

namespace GameOfGoose.Core.Logging
{
    /// <summary>
    /// Formats game output strings for the Game of Goose.
    /// </summary>
    public class GameFormatter : IGameFormatter
    {
        /// <summary>
        /// Formats a board position as a string.
        /// </summary>
        /// <param name="position">The board position to format.</param>
        /// <returns>A formatted position string, e.g. "S10" or "Start".</returns>
        public string FormatPosition(int position) => position == 0 ? "Start" : $"S{position}";

        /// <summary>
        /// Formats the result of a player's turn, including arrow notation if the piece was moved by a space action.
        /// </summary>
        /// <param name="roll1">The value of the first die.</param>
        /// <param name="roll2">The value of the second die.</param>
        /// <param name="positionAfterBounce">The position after movement and bounce, before space action.</param>
        /// <param name="finalPosition">The final position after all rules have been applied.</param>
        /// <returns>A formatted turn result string, e.g. "2+4: S6->S12" or "1+3: S4".</returns>
        public string FormatTurnResult(int roll1, int roll2, int positionAfterBounce, int finalPosition)
        {
            if (finalPosition != positionAfterBounce)
                return $"{roll1}+{roll2}: {FormatPosition(positionAfterBounce)}->{FormatPosition(finalPosition)}";

            return $"{roll1}+{roll2}: {FormatPosition(finalPosition)}";
        }

        /// <summary>
        /// Formats a skipped turn for a player.
        /// </summary>
        /// <param name="position">The current position of the piece.</param>
        /// <returns>A formatted skip turn string, e.g. "/ :S10".</returns>
        public string FormatSkipTurn(int position) => $"/ :{FormatPosition(position)}";

        /// <summary>
        /// Formats the header row showing all piece labels.
        /// </summary>
        /// <param name="playerCount">The number of players in the game.</param>
        /// <returns>A formatted header string, e.g. "PIECE 1\tPIECE 2\tPIECE 3\tPIECE 4".</returns>
        public string FormatHeader(int playerCount) =>
            string.Join("\t", Enumerable.Range(1, playerCount).Select(i => $"PIECE {i}"));

        /// <summary>
        /// Formats the turn start label.
        /// </summary>
        /// <param name="turnNumber">The current turn number.</param>
        /// <returns>A formatted turn start string, e.g. "Turn 1".</returns>
        public string FormatTurnStart(int turnNumber) => $"Turn {turnNumber}";

        /// <summary>
        /// Formats all turn results for a round as a tab-separated string.
        /// </summary>
        /// <param name="results">The individual turn result strings.</param>
        /// <returns>A tab-separated string of all turn results.</returns>
        public string FormatTurnResults(IEnumerable<string> results) => string.Join("\t", results);

        /// <summary>
        /// Formats the winner announcement.
        /// </summary>
        /// <param name="playerName">The name of the winning player.</param>
        /// <returns>A formatted winner string, e.g. "Player 1 has won!".</returns>
        public string FormatWinner(string playerName) => $"{playerName} has won!";

        /// <summary>
        /// Formats the press enter prompt between turns.
        /// </summary>
        /// <param name="turnNumber">The next turn number.</param>
        /// <returns>A formatted press enter string, e.g. "[Press ENTER to play Turn 2]".</returns>
        public string FormatPressEnter(int turnNumber) => $"[Press ENTER to play Turn {turnNumber}]";

        /// <summary>
        /// Formats the press enter prompt at the end of the game.
        /// </summary>
        /// <returns>A formatted finish string, e.g. "Press ENTER to FINISH GAME".</returns>
        public string FormatPressEnterFinish() => "Press ENTER to FINISH GAME";
    }
}