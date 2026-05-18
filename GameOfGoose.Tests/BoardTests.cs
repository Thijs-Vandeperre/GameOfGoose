using System.Collections.Generic;
using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests
{
    /// <summary>
    /// Contains unit tests for the Board class.
    /// </summary>
    public class BoardTests
    {
        private readonly IReadOnlyDictionary<int, ISpace> _spaces;
        private readonly Board _board;

        /// <summary>
        /// Sets up a Board instance with a space dictionary for testing.
        /// </summary>
        public BoardTests()
        {
            _spaces = new Dictionary<int, ISpace> {
                { 6, new Bridge(6, 12) },
                { 19, new Inn(19, 1) }
            };
            _board = new Board(_spaces);
        }

        /// <summary>
        /// Verifies that GetSpace returns the correct space.
        /// </summary>
        [Fact]
        public void GetSpace_ReturnsCorrectSpace()
        {
            var space = _board.GetSpace(6);
            Assert.IsType<Bridge>(space);

            space = _board.GetSpace(19);
            Assert.IsType<Inn>(space);
        }
    }
}