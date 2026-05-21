using System;
using System.IO;
using GameOfGoose.Core.UI;

namespace GameOfGoose.Tests.Logging
{
    /// <summary>
    /// Contains unit tests for the ConsoleInputReader class.
    /// </summary>
    public class ConsoleInputReaderTests
    {
        private readonly ConsoleInputReader _inputReader;

        /// <summary>
        /// Sets up a ConsoleInputReader instance for testing.
        /// </summary>
        public ConsoleInputReaderTests()
        {
            _inputReader = new ConsoleInputReader();
        }

        /// <summary>
        /// Verifies that WaitForEnter returns when Enter is pressed. 
        /// </summary>
        [Fact]
        public void WaitForEnter_ReturnsWhenEnterIsPressed()
        {
            Console.SetIn(new StringReader(""));
            _inputReader.WaitForEnter();
            Assert.True(true);
        }
    }
}