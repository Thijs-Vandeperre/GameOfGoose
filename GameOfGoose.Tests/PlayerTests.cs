using GameOfGoose.Core;

namespace GameOfGoose.Tests
{
    /// <summary>
    /// Contains unit tests for the Player class.
    /// </summary>
    public class PlayerTests
    {
        /// <summary>
        /// Verifies that the default values of a Player are correct.
        /// </summary>
        /// <param name="playerNumber">The number of players used to create the player.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Player_HasCorrectDefaultValues(int playerNumber)
        {
            var player = new Player(new Piece(playerNumber), playerNumber);
            Assert.NotNull(player.Piece);
            Assert.Equal(playerNumber, player.PlayerNumber);
            Assert.Equal($"Player {playerNumber}", player.Name);
        }
    }
}