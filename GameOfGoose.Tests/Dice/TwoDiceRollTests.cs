using GameOfGoose.Core.Dice;

namespace GameOfGoose.Tests.Dice
{
    /// <summary>
    /// Contains unit tests for the TwoDiceRoll class.
    /// </summary>
    public class TwoDiceRollTests
    {
        private readonly Die _die;
        private readonly TwoDiceRoll _twoDiceRoll;

        /// <summary>
        /// Sets up a Die and TwoDiceRoll instance for testing.  
        /// </summary>
        public TwoDiceRollTests()
        {
            _die = new Die();
            _twoDiceRoll = new TwoDiceRoll(_die);
        }

        /// <summary>
        /// Verifies that DoubleRoll returns two values between 1 and 6 over 1000 iterations.
        /// </summary>
        [Fact]
        public void DoubleRoll_ReturnsTwoValuesBetweenOneAndSix()
        {
            for (int i = 0; i < 1000; i++)
            {
                var (roll1, roll2) = _twoDiceRoll.DoubleRoll();
                Assert.InRange(roll1, 1, 6);
                Assert.InRange(roll2, 1, 6);
            }
        }
    }
}