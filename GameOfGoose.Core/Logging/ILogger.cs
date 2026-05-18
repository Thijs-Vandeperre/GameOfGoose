namespace GameOfGoose.Core.Logging
{
    /// <summary>
    /// Defines a contract for logging messages.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message);
    }
}