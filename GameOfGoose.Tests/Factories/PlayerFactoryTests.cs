using GameOfGoose.Core;


namespace GameOfGoose.Tests
{
    /// <summary>
    /// Contains unit tests for the PlayerFactory.
    /// </summary>
    public class PlayerFactoryTests
    {
        /// <summary>
        /// Verifies that CreatePlayers returns the correct number of players.
        /// </summary>
        /// <param name="numberOfPlayers">The number of players that are created.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CreatePlayers_ReturnsCorrectNumberOfPlayers(int numberOfPlayers)
        {
            var pieces = PieceFactory.CreatePieces(numberOfPlayers);
            var players = PlayerFactory.CreatePlayers(pieces);
            Assert.Equal(numberOfPlayers, players.Count);
        }

        /// <summary>
        /// Verifies that the created players are numbered correctly.
        /// </summary>
        /// <param name="numberOfPlayers">The number of players that are created.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CreatePlayers_PlayersAreNumberedCorrectly(int numberOfPlayers)
        {
            var pieces = PieceFactory.CreatePieces(numberOfPlayers);
            var players = PlayerFactory.CreatePlayers(pieces);
            for (int i = 0; i < players.Count; i++)
            {
                Assert.Equal(i + 1, players[i].PlayerNumber);
            }
        }

        /// <summary>
        /// Verifies that the players have the correct pieces assigned.
        /// </summary>
        /// <param name="numberOfPlayers">The number of players that are created.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CreatePlayers_PlayersHaveCorrectPiecesAssigned(int numberOfPlayers)
        {
            var pieces = PieceFactory.CreatePieces(numberOfPlayers);
            var players = PlayerFactory.CreatePlayers(pieces);
            for (int i = 0; i < players.Count; i++)
            {
                Assert.Equal(pieces[i], players[i].Piece);
            }
        }

        /// <summary>
        /// Verifies that CreatePlayers returns an empty list when an empty list is passed. 
        /// </summary>
        [Fact]
        public void CreatePlayers_WithEmptyList_ReturnsEmptyList()
        {
            var pieces = PieceFactory.CreatePieces(0);
            var players = PlayerFactory.CreatePlayers(pieces);
            Assert.Empty(players);
        }
    }
}