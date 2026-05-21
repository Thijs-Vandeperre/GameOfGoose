namespace GameOfGoose.Core.UI
{
    /// <summary>
    /// Defines a contract for reading user input.
    /// </summary>
    public interface IInputReader
    {
        /// <summary>
        /// Waits for the user to press Enter.
        /// </summary>
        public void WaitForEnter();
    }
}