namespace GameOfGoose.Core.Dice
{
    /// <summary>
    /// Provides functionality to roll two dice using a die abstraction.
    /// </summary>
    public class TwoDiceRoll : IDiceRoll
    {
        private readonly IDie _die;

        /// <summary>
        /// Initializes a new instance of TwoDiceRoll with the specified die.   
        /// </summary>
        /// <param name="die">The die used to generate random roll values.</param>
        public TwoDiceRoll(IDie die)
        {
            _die = die;
        }

        /// <summary>
        /// Rolls the die twice and returns both results.
        /// </summary>
        /// <returns>A named tuple containing Roll1 and Roll2 values.</returns>
        public (int Roll1, int Roll2) DoubleRoll()
        {
            return (_die.Roll(), _die.Roll());
        }
    }
}