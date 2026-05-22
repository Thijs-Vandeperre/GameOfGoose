namespace GameOfGoose.Core.Dice
{
    /// <summary>
    /// Represents a single die that can be rolled.
    /// </summary>
    public interface IDie
    {
        /// <summary>
        /// Rolls the die and returns a value between 1 and 6.
        /// </summary>
        /// <returns>An integer between 1 and 6.</returns>
        int Roll();
    }
}