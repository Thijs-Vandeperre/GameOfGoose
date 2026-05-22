using System.Collections.Generic;

namespace GameOfGoose.Core.Dice
{
    /// <summary>
    /// Fake implementation of IDie used for unit testing.
    /// Returns predefined values instead of random numbers.
    /// </summary>
    public class FakeDie : IDie
    {
        private readonly Queue<int> _values;

        /// <summary>
        /// Initializes a new fake die with predefined roll values.
        /// </summary>
        /// <param name="values">Sequence of values to return on each roll.</param>
        public FakeDie(params int[] values)
        {
            _values = new Queue<int>(values);
        }

        /// <summary>
        /// Returns the next predefined value in the sequence.
        /// </summary>
        /// <returns>The next fake roll value.</returns>
        public int Roll() => _values.Dequeue();
    }
}