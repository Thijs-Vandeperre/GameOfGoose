using System;
using System.IO;
using GameOfGoose.Core.UI;

namespace GameOfGoose.Tests.Logging
{
    /// <summary>
    /// Contains unit tests for the ConsoleLogger class.
    /// </summary>
    public class ConsoleLoggerTests
    {
        private readonly ConsoleLogger _logger;

        /// <summary>
        /// Sets up a ConsoleLogger instance for testing.
        /// </summary>
        public ConsoleLoggerTests()
        {
            _logger = new ConsoleLogger();
        }

        /// <summary>
        /// Verifies that Log writes the message to the console output. 
        /// </summary>
        [Fact]
        public void Log_WritesMessageToConsoleOutput()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            string message = "Hello, World!";
            _logger.Log(message);

            Assert.Contains(message, output.ToString());
        }
    }
}