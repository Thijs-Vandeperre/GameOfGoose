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
        /// Verifies that GetSpace returns a Bridge at position 6.
        /// </summary>
        [Fact]
        public void GetSpace_AtBridgePosition_ReturnsBridge()
        {
            var space = _board.GetSpace(6);
            Assert.IsType<Bridge>(space);
        }

        /// <summary>
        /// Verifies that GetSpace returns an Inn at position 19.
        /// </summary>
        [Fact]
        public void GetSpace_AtInnPosition_ReturnsInn()
        {
            var space = _board.GetSpace(19);
            Assert.IsType<Inn>(space);
        }
    }
}