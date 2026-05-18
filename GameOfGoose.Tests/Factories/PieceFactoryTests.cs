using GameOfGoose.Core.Factories;

namespace GameOfGoose.Tests.Factories
{
    /// <summary>
    /// Contains unit tests for the PieceFactory.
    /// </summary>
    public class PieceFactoryTests
    {
        /// <summary>
        /// Verifies that CreatePieces returns the correct number of pieces.
        /// </summary>
        /// <param name="numberOfPieces">The number of pieces that are created.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CreatePieces_ReturnsCorrectNumberOfPieces(int numberOfPieces)
        {
            var pieces = PieceFactory.CreatePieces(numberOfPieces);
            Assert.Equal(numberOfPieces, pieces.Count);
        }

        /// <summary>
        /// Verifies that the created pieces are numbered correctly.
        /// </summary>
        /// <param name="numberOfPieces">The number of pieces that are created.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CreatePieces_PiecesAreNumberedCorrectly(int numberOfPieces)
        {
            var pieces = PieceFactory.CreatePieces(numberOfPieces);
            for (int i = 0; i < pieces.Count; i++)
            {
                Assert.Equal(i + 1, pieces[i].PieceNumber);
            }
        }

        /// <summary>
        /// Verifies that CreatePieces returns an empty list when 0 is passed.
        /// </summary>
        [Fact]
        public void CreatePieces_WithZeroPieces_ReturnsEmptyList()
        {
            var pieces = PieceFactory.CreatePieces(0);
            Assert.Empty(pieces);
        }
    }
}