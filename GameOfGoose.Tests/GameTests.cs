using System.Collections.Generic;
using System.Linq;
using GameOfGoose.Core;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Factories;
using GameOfGoose.Core.Logging;

namespace GameOfGoose.Tests
{
    /// <summary>
    /// Contains unit tests for the Game class.
    /// </summary>
    public class GameTests
    {
        private readonly Game _game;
        private readonly List<Player> _players;
        private readonly Board _board;

        /// <summary>
        /// A no-op input reader for testing that does not block on input.
        /// </summary>
        private class NoOpInputReader : IInputReader
        {
            /// <summary>
            /// Does nothing — used to prevent blocking in tests.
            /// </summary>
            public void WaitForEnter() { }
        }

        /// <summary>
        /// A no-op logger for testing that does not write to console.
        /// </summary>
        private class NoOpLogger : ILogger
        {
            /// <summary>
            /// Does nothing — used to suppress console output in tests.
            /// </summary>
            /// <param name="message">The message to log.</param>
            public void Log(string message) { }
        }

        /// <summary>
        /// Sets up a Game instance with players, board, dice roll, no-op logger and no-op input reader for testing.
        /// </summary>
        public GameTests()
        {
            var pieces = PieceFactory.CreatePieces(4);
            _players = PlayerFactory.CreatePlayers(pieces);
            _board = BoardFactory.CreateBoard(pieces);
            var diceRoll = new TwoDiceRoll(new Die());
            _game = new Game(_players, _board, diceRoll, new NoOpLogger(), new NoOpInputReader());
        }

        #region HandleFirstTurn

        /// <summary>
        /// Verifies that HandleFirstTurn moves the piece to the correct position for special first turn rolls.
        /// </summary>
        /// <param name="roll1">The value of the first die.</param>
        /// <param name="roll2">The value of the second die.</param>
        /// <param name="expectedPosition">The expected position after the special roll.</param>
        [Theory]
        [InlineData(5, 4, 26)]
        [InlineData(4, 5, 26)]
        [InlineData(6, 3, 53)]
        [InlineData(3, 6, 53)]
        public void HandleFirstTurn_SpecialRoll_MovesPieceToCorrectPosition(int roll1, int roll2, int expectedPosition)
        {
            var piece = _players[0].Piece;
            _game.HandleFirstTurn(piece, roll1, roll2);
            Assert.Equal(expectedPosition, piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that HandleFirstTurn returns true for special first turn rolls.
        /// </summary>
        /// <param name="roll1">The value of the first die.</param>
        /// <param name="roll2">The value of the second die.</param>
        [Theory]
        [InlineData(5, 4)]
        [InlineData(4, 5)]
        [InlineData(6, 3)]
        [InlineData(3, 6)]
        public void HandleFirstTurn_SpecialRoll_ReturnsTrue(int roll1, int roll2)
        {
            var piece = _players[0].Piece;
            var result = _game.HandleFirstTurn(piece, roll1, roll2);
            Assert.True(result);
        }

        /// <summary>
        /// Verifies that HandleFirstTurn returns false for non-special rolls.
        /// </summary>
        /// <param name="roll1">The value of the first die.</param>
        /// <param name="roll2">The value of the second die.</param>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 3)]
        [InlineData(6, 6)]
        public void HandleFirstTurn_WithOtherRoll_ReturnsFalse(int roll1, int roll2)
        {
            var piece = _players[0].Piece;
            var result = _game.HandleFirstTurn(piece, roll1, roll2);
            Assert.False(result);
        }

        /// <summary>
        /// Verifies that HandleFirstTurn does not move the piece for non-special rolls.
        /// </summary>
        /// <param name="roll1">The value of the first die.</param>
        /// <param name="roll2">The value of the second die.</param>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 3)]
        [InlineData(6, 6)]
        public void HandleFirstTurn_WithOtherRoll_DoesNotMovePiece(int roll1, int roll2)
        {
            var piece = _players[0].Piece;
            _game.HandleFirstTurn(piece, roll1, roll2);
            Assert.Equal(0, piece.CurrentPosition);
        }

        #endregion

        #region HandleBounce

        /// <summary>
        /// Verifies that HandleBounce returns the correct bounced position when a piece overshoots the end.
        /// </summary>
        /// <param name="newPosition">The position that overshoots the end.</param>
        /// <param name="expectedPosition">The expected bounced position.</param>
        [Theory]
        [InlineData(64, 62)]
        [InlineData(70, 56)]
        [InlineData(75, 51)]
        public void HandleBounce_OvershootingEnd_BouncesBackToCorrectPosition(int newPosition, int expectedPosition)
        {
            var piece = _players[0].Piece;
            var result = _game.HandleBounce(piece, newPosition);
            Assert.Equal(expectedPosition, result);
        }

        /// <summary>
        /// Verifies that HandleBounce sets IsMovingForward to false on the piece.
        /// </summary>
        [Fact]
        public void HandleBounce_SetsIsMovingForwardToFalse()
        {
            var piece = _players[0].Piece;
            _game.HandleBounce(piece, 70);
            Assert.False(piece.IsMovingForward);
        }

        /// <summary>
        /// Verifies that HandleBounce correctly calculates position for various overshoots.
        /// </summary>
        /// <param name="newPosition">The position that overshoots the end.</param>
        /// <param name="expectedPosition">The expected bounced position.</param>
        [Theory]
        [InlineData(64, 62)]
        [InlineData(65, 61)]
        [InlineData(69, 57)]
        public void HandleBounce_VariousOvershoots_CalculatesCorrectPosition(int newPosition, int expectedPosition)
        {
            var piece = _players[0].Piece;
            var result = _game.HandleBounce(piece, newPosition);
            Assert.Equal(expectedPosition, result);
        }

        #endregion

        #region NextTurn - SkipTurns

        /// <summary>
        /// Verifies that NextTurn decrements SkipTurns by one when the piece has turns to skip.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_DecrementsSkipTurns()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 2;
            _game.NextTurn(player);
            Assert.Equal(1, player.Piece.SkipTurns);
        }

        /// <summary>
        /// Verifies that NextTurn does not move the piece when it has turns to skip.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_DoesNotMovePiece()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 1;
            player.Piece.MoveTo(10);
            _game.NextTurn(player);
            Assert.Equal(10, player.Piece.CurrentPosition);
        }

        /// <summary>
        /// Verifies that NextTurn returns the correct skip format string when the piece has turns to skip.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_ReturnsSkipFormat()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 1;
            player.Piece.MoveTo(10);
            var result = _game.NextTurn(player);
            Assert.Equal("/ :S10", result);
        }

        /// <summary>
        /// Verifies that NextTurn returns the correct skip format string when the piece is at the start position.
        /// </summary>
        [Fact]
        public void NextTurn_WithSkipTurns_AtStart_ReturnsStartFormat()
        {
            var player = _players[0];
            player.Piece.SkipTurns = 1;
            var result = _game.NextTurn(player);
            Assert.Equal("/ :Start", result);
        }

        #endregion

        #region NextTurn - Normal Move

        /// <summary>
        /// Verifies that NextTurn moves the piece on a normal turn.
        /// </summary>
        [Fact]
        public void NextTurn_NormalTurn_MovesPiece()
        {
            var player = _players[0];
            player.Piece.MoveTo(10);
            _game.NextTurn(player);
            Assert.NotEqual(10, player.Piece.CurrentPosition);
        }

        #endregion

        #region IsMovingForward Reset

        /// <summary>
        /// Verifies that NextTurn resets IsMovingForward to true at the start of each turn.
        /// </summary>
        [Fact]
        public void NextTurn_AfterBounce_ResetsIsMovingForwardToTrue()
        {
            var player = _players[0];
            player.Piece.IsMovingForward = false;
            player.Piece.MoveTo(10);
            _game.NextTurn(player);
            Assert.True(player.Piece.IsMovingForward);
        }

        #endregion

        #region Death resets to start

        /// <summary>
        /// Verifies that the Death space is correctly placed at position 58 on the board.
        /// </summary>
        [Fact]
        public void Board_DeathSpaceAt58_IsCorrectType()
        {
            var deathSpace = _board.GetSpace(58);
            Assert.IsType<GameOfGoose.Core.Spaces.Death>(deathSpace);
        }

        #endregion

        #region FormatPosition

        /// <summary>
        /// Verifies that FormatPosition returns "START" for position 0.
        /// </summary>
        [Fact]
        public void FormatPosition_WithZero_ReturnsStart()
        {
            var result = _game.FormatPosition(0);
            Assert.Equal("Start", result);
        }

        /// <summary>
        /// Verifies that FormatPosition returns the correct position string for non-zero positions.
        /// </summary>
        /// <param name="position">The board position to format.</param>
        /// <param name="expected">The expected formatted string.</param>
        [Theory]
        [InlineData(1, "S1")]
        [InlineData(32, "S32")]
        [InlineData(63, "S63")]
        public void FormatPosition_WithNonZero_ReturnsCorrectString(int position, string expected)
        {
            var result = _game.FormatPosition(position);
            Assert.Equal(expected, result);
        }

        #endregion

        #region Start


        // TODO: Add ITwoDiceRoll interface and FixedDiceRoll mock to enable deterministic Start tests
        /// <summary>
        /// Verifies that Start ends the game when a player reaches the end position.
        /// </summary>
        // [Fact]
        // public void Start_WhenPlayerWins_SetsHasWonToTrue()
        // {
        //     foreach (var player in _players)
        //     {
        //         player.Piece.MoveTo(62);
        //     }
        //     _game.Start();
        //     Assert.True(_players.Any(p => p.Piece.HasWon));
        // }

        #endregion
    }
}