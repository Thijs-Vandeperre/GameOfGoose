using System;

namespace GameOfGoose.Core.Dice
{
    /// <summary>
    /// Represents a single die.  
    /// </summary>
    public class Die
    {
        /// <summary>
        /// Rolls the die and returns a value between 1 and 6. 
        /// </summary>
        /// <returns>A random integer between 1 and 6. </returns>
        public int Roll()
        {
            return Random.Shared.Next(1, 7);
        }
    }
}