using System.Collections.Generic;
using System.Linq;

namespace GameOfGoose.Core.Logging
{
    /// <summary>
    /// Formats game output strings for the Game of Goose.
    /// </summary>
    public class GameFormatter : IGameFormatter
    {
        public string FormatPosition(int position) => position == 0 ? "Start" : $"S{position}";

        public string FormatTurnResult(int roll1, int roll2, int positionAfterBounce, int finalPosition)
        {
            if (finalPosition != positionAfterBounce)
                return $"{roll1}+{roll2}: {FormatPosition(positionAfterBounce)}->{FormatPosition(finalPosition)}";

            return $"{roll1}+{roll2}: {FormatPosition(finalPosition)}";
        }

        public string FormatSkipTurn(int position) => $"/ :{FormatPosition(position)}";

        public string FormatHeader(int playerCount) =>
            string.Join("\t", Enumerable.Range(1, playerCount).Select(i => $"PIECE {i}"));

        public string FormatTurnStart(int turnNumber) => $"Turn {turnNumber}";

        public string FormatTurnResults(IEnumerable<string> results) => string.Join("\t", results);

        public string FormatWinner(string playerName) => $"{playerName} has won!";

        public string FormatPressEnter(int turnNumber) => $"[Press ENTER to play Turn {turnNumber}]";

        public string FormatPressEnterFinish() => "Press ENTER to FINISH GAME";
    }
}