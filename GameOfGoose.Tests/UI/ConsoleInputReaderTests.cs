using System;
using System.IO;
using GameOfGoose.Core.UI;

namespace GameOfGoose.Tests.UI
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
        /// Verifies that WaitForEnter reads from stdin without throwing.
        /// ConsoleInputReader is a thin wrapper around Console.ReadLine; 
        /// the meaningful behavior (interaction with the real console) is not unit-testable.
        /// This test guards against regressions in the wrapper itself.
        /// </summary>
        [Fact]
        public void WaitForEnter_WithSimulatedInput_DoesNotThrow()
        {
            Console.SetIn(new StringReader(string.Empty));
            var ex = Record.Exception(() => _inputReader.WaitForEnter());
            Assert.Null(ex);
        }
    }
}