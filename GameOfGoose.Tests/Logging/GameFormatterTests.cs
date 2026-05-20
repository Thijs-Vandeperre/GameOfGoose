using GameOfGoose.Core.Logging;

namespace GameOfGoose.Tests.Logging
{
    /// <summary>
    /// Contains unit tests for the GameFormatter class.
    /// </summary>
    public class GameFormatterTests
    {
        private readonly GameFormatter _formatter;

        public GameFormatterTests()
        {
            _formatter = new GameFormatter();
        }

        #region FormatPosition

        /// <summary>
        /// Verifies formatting of position 0.
        /// </summary>
        [Fact]
        public void FormatPosition_Zero_ReturnsStart()
        {
            Assert.Equal("Start", _formatter.FormatPosition(0));
        }

        /// <summary>
        /// Verifies formatting of non-zero positions.
        /// </summary>
        [Theory]
        [InlineData(1, "S1")]
        [InlineData(32, "S32")]
        [InlineData(63, "S63")]
        public void FormatPosition_NonZero_ReturnsCorrect(int input, string expected)
        {
            Assert.Equal(expected, _formatter.FormatPosition(input));
        }

        #endregion

        #region FormatTurnResult

        /// <summary>
        /// Verifies that no arrow is shown when position did not change after bounce.
        /// </summary>
        [Fact]
        public void FormatTurnResult_NoPositionChange_ReturnsWithoutArrow()
        {
            Assert.Equal("2+3: S10", _formatter.FormatTurnResult(2, 3, 10, 10));
        }

        /// <summary>
        /// Verifies that arrow is shown when position changed after space action.
        /// </summary>
        [Fact]
        public void FormatTurnResult_PositionChanged_ReturnsWithArrow()
        {
            Assert.Equal("2+4: S6->S12", _formatter.FormatTurnResult(2, 4, 6, 12));
        }

        /// <summary>
        /// Verifies formatting when final position is Start.
        /// </summary>
        [Fact]
        public void FormatTurnResult_FinalPositionStart_ReturnsWithArrowToStart()
        {
            Assert.Equal("5+3: S58->Start", _formatter.FormatTurnResult(5, 3, 58, 0));
        }

        #endregion

        #region FormatSkipTurn

        /// <summary>
        /// Verifies skip turn format output.
        /// </summary>
        [Fact]
        public void FormatSkipTurn_ReturnsCorrectFormat()
        {
            Assert.Equal("/ :S10", _formatter.FormatSkipTurn(10));
        }

        #endregion

        #region FormatTurnStart

        /// <summary>
        /// Verifies turn start format output.
        /// </summary>
        [Fact]
        public void FormatTurnStart_ReturnsCorrectFormat()
        {
            Assert.Equal("Turn 5", _formatter.FormatTurnStart(5));
        }

        #endregion

        #region FormatPressEnter

        /// <summary>
        /// Verifies press enter format output.
        /// </summary>
        [Fact]
        public void FormatPressEnter_ReturnsCorrectFormat()
        {
            Assert.Equal("[Press ENTER to play Turn 3]", _formatter.FormatPressEnter(3));
        }

        #endregion

        #region FormatPressEnterFinish

        /// <summary>
        /// Verifies press enter finish format output.
        /// </summary>
        [Fact]
        public void FormatPressEnterFinish_ReturnsCorrectFormat()
        {
            Assert.Equal("Press ENTER to FINISH GAME", _formatter.FormatPressEnterFinish());
        }

        #endregion

        #region FormatWinner

        /// <summary>
        /// Verifies winner format output.
        /// </summary>
        [Fact]
        public void FormatWinner_ReturnsCorrectFormat()
        {
            Assert.Equal("Player 1 has won!", _formatter.FormatWinner("Player 1"));
        }

        #endregion
    }
}