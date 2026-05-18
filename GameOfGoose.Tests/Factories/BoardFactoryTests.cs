using System.Collections.Generic;
using GameOfGoose.Core;
using GameOfGoose.Core.Factories;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Factories
{
    /// <summary>
    /// Contains unit tests for the BoardFactory.
    /// </summary>
    public class BoardFactoryTests
    {
        private readonly IReadOnlyList<Piece> _pieces;
        private readonly Board _board;

        /// <summary>
        /// Sets up a board instance for testing.
        /// </summary>
        public BoardFactoryTests()
        {
            _pieces = PieceFactory.CreatePieces(4);
            _board = BoardFactory.CreateBoard(_pieces);
        }

        /// <summary>
        /// Verifies that CreateBoard returns the correct space at special positions.
        /// </summary>
        [Fact]
        public void CreateBoard_ReturnsCorrectSpaceAtSpecialPositions()
        {
            Assert.IsType<Bridge>(_board.GetSpace(6));
            Assert.IsType<Inn>(_board.GetSpace(19));
            Assert.IsType<Well>(_board.GetSpace(31));
            Assert.IsType<Maze>(_board.GetSpace(42));
            Assert.IsType<Prison>(_board.GetSpace(52));
            Assert.IsType<Death>(_board.GetSpace(58));
            Assert.IsType<End>(_board.GetSpace(63));
        }

        /// <summary>
        /// Verifies that CreateBoard returns the correct space at goose positions.
        /// </summary>
        [Fact]
        public void CreateBoard_ReturnsGooseAtGoosePositions()
        {
            var gooseSpaces = new List<int> { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59};
            foreach (var position in gooseSpaces)
            {
                Assert.IsType<Goose>(_board.GetSpace(position));
            }
        }

        /// <summary>
        /// Verifies that CreateBoard returns the correct space at regular positions.
        /// </summary>
        [Fact]
        public void CreateBoard_ReturnsRegularAtRegularPositions()
        {
            var regularSpaces = new List<int> { 0, 1, 2, 3, 4, 7, 8, 10, 11, 12, 13, 15, 16, 17, 20, 21, 22, 24, 25, 26, 28, 29, 30, 33, 34, 35, 37, 38, 39, 40, 43, 44, 46, 47, 48, 49, 51, 53, 55, 56, 57, 60, 61, 62 };
            foreach (var position in regularSpaces)
            {
                Assert.IsType<Regular>(_board.GetSpace(position));
            }
        }
    }
}