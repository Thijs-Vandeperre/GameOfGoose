using System;
using GameOfGoose.Core.Dice;

namespace GameOfGoose.Tests.Dice
{
    /// <summary>
    /// Contains unit tests for the FakeDie class.
    /// </summary>
    public class FakeDieTests
    {
        /// <summary>
        /// Verifies that Roll returns the predefined value.
        /// </summary>
        [Fact]
        public void Roll_WithSingleValue_ReturnsValue()
        {
            var die = new FakeDie(4);
            var result = die.Roll();
            Assert.Equal(4, result);
        }

        /// <summary>
        /// Verifies that Roll returns values in the order they were provided.
        /// </summary>
        [Fact]
        public void Roll_WithMultipleValues_ReturnsValuesInOrder()
        {
            var die = new FakeDie(2, 5, 6);

            var first = die.Roll();
            var second = die.Roll();
            var third = die.Roll();

            Assert.Equal(2, first);
            Assert.Equal(5, second);
            Assert.Equal(6, third);
        }

        /// <summary>
        /// Verifies that Roll throws an exception when no values remain.
        /// </summary>
        [Fact]
        public void Roll_WithNoRemainingValues_ThrowsException()
        {
            var die = new FakeDie();
            Assert.Throws<InvalidOperationException>(() => die.Roll());
        }
    }
}