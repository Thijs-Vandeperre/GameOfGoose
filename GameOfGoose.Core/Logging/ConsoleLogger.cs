using System;

namespace GameOfGoose.Core.Logging
{
    /// <summary>
    /// Implements the ILogger interface for logging messages in console.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Logs the specified message to console.
        /// </summary>
        /// <param name="message">The message to log to console.</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}