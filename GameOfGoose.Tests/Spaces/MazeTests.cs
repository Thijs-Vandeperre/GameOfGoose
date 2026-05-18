using GameOfGoose.Core;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Tests.Spaces
{
    /// <summary>
    /// Contains unit tests for the Maze class.
    /// </summary>
    public class MazeTests
    {
        private readonly Maze _maze;
        private readonly Piece _piece;

        /// <summary>
        /// Sets up a maze and a piece instance for testing.
        /// </summary>
        public MazeTests()
        {
            _maze = new Maze(42, 39);
            _piece = new Piece(1);
        }

        /// <summary>
        /// Verifies that SpaceAction moves the piece to the correct board position.
        /// </summary>
        [Fact]
        public void SpaceAction_MovesPieceToCorrectDestination()
        {
            _maze.SpaceAction(_piece, 2);
            Assert.Equal(39, _piece.CurrentPosition);
        }
    }
}