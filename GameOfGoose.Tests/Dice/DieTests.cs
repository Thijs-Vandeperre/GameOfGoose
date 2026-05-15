using GameOfGoose.Core.Dice;

namespace GameOfGoose.Tests.Dice
{
    /// <summary>
    /// Contains unit tests for the Die class. 
    /// </summary>
    public class DieTests
    {
        private readonly Die _die;

        /// <summary>
        /// Sets up a Die instance for testing.
        /// </summary>
        public DieTests()
        {
            _die = new Die();
        }

        /// <summary>
        /// Verifies that Roll returns a value between 1 and 6 over 1000 iterations.  
        /// </summary>
        [Fact]
        public void Roll_ReturnsValueBetweenOneAndSix()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = _die.Roll();
                Assert.InRange(result, 1, 6);
            }               
        }

    }
}