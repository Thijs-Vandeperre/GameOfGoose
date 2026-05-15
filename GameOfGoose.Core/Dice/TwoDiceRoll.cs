namespace GameOfGoose.Core.Dice
{
    /// <summary>
    /// Represents a roll of two dice.   
    /// </summary>
    public class TwoDiceRoll
    {
        private readonly Die _die;

        /// <summary>
        /// Initializes a new instance of TwoDiceRoll with the specified die.   
        /// </summary>
        /// <param name="die">The die used to generate random roll values.</param>
        public TwoDiceRoll(Die die)
        {
            _die = die;
        }

        /// <summary>
        /// Rolls the die twice and returns both results as a named tuple.
        /// </summary>
        /// <returns>A named tuple containing Roll1 and Roll2 values.</returns>
        public (int Roll1, int Roll2) DoubleRoll()
        {
            int roll1 = _die.Roll();
            int roll2 = _die.Roll();
            return (roll1, roll2);
        }
    }
}