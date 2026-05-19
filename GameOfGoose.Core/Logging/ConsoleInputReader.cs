using System;

namespace GameOfGoose.Core.Logging
{
    /// <summary>
    /// Implements IInputReader for reading user input from the console.
    /// </summary>
    public class ConsoleInputReader : IInputReader
    {
        /// <summary>
        /// Waits for the user to press Enter in the console.
        /// </summary>
        public void WaitForEnter()
        {
            Console.ReadLine();
        }
    }
}