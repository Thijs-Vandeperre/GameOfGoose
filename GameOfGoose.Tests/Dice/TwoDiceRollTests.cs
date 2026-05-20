using GameOfGoose.Core.Dice;

namespace GameOfGoose.Tests.Dice
{
    /// <summary>
    /// Contains unit tests for the TwoDiceRoll class.
    /// </summary>
    public class TwoDiceRollTests
    {
        /// <summary>
        /// Verifies that DoubleRoll returns the expected values from the injected die.
        /// </summary>
        [Theory]
        [InlineData(3, 5)]
        [InlineData(1, 4)]
        [InlineData(6, 2)]
        public void DoubleRoll_ReturnsExpectedValues(int roll1, int roll2)
        {
            var diceRoll = new TwoDiceRoll(new FakeDie(roll1, roll2));

            var result = diceRoll.DoubleRoll();
            Assert.Equal(roll1, result.Roll1);
            Assert.Equal(roll2, result.Roll2);
        }
    }
}