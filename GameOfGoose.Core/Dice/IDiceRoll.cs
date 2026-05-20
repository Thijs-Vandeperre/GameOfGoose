namespace GameOfGoose.Core.Dice
{
    /// <summary>
    /// Defines functionality for rolling two dice and returning both results.
    /// </summary>
    public interface IDiceRoll
    {
        /// <summary>
        /// Rolls two dice and returns their values.
        /// </summary>
        /// <returns>A named tuple containing the values of the first die (<c>Roll1</c>) and second die (<cRoll2</c>).></returns>
        (int Roll1, int Roll2) DoubleRoll();
    }
}