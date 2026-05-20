using System.Collections.Generic;

namespace GameOfGoose.Core.Logging
{
    /// <summary>
    /// Defines formatting for game output strings.
    /// </summary>
    public interface IGameFormatter
    {
        string FormatPosition(int position);
        string FormatTurnResult(int roll1, int roll2, int positionAfterBounce, int finalPosition);
        string FormatSkipTurn(int position);
        string FormatHeader(int playerCount);
        string FormatTurnStart(int turnNumber);
        string FormatTurnResults(IEnumerable<string> results);
        string FormatWinner(string playerName);
        string FormatPressEnter(int turnNumber);
        string FormatPressEnterFinish();
    }
}