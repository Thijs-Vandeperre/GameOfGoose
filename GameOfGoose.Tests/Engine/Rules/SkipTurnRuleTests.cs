using System.Collections.Generic;
using GameOfGoose.Core;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Factories;

namespace GameOfGoose.Tests.Engine.Rules
{
    /// <summary>
    /// Contains unit tests for the SkipTurnRule class.
    /// </summary>
    public class SkipTurnRuleTests
    {
        private readonly SkipTurnRule _rule;
        private readonly IReadOnlyList<Piece> _pieces;
        private readonly IReadOnlyList<Player> _players;
        private readonly Board _board;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipTurnRuleTests"/> class.
        /// </summary>
        public SkipTurnRuleTests()
        {
            _rule = new SkipTurnRule();
            _pieces = PieceFactory.CreatePieces(1);
            _players = PlayerFactory.CreatePlayers(_pieces);
            _board = BoardFactory.CreateBoard(_pieces);
        }

        /// <summary>
        /// Creates a game context for the specified player.
        /// </summary>
        /// <param name="player">The player for whom the context is created.</param>
        /// <returns>A game context containing the player, board and turn information.</returns>
        private GameContext CreateContext(Player player) => new GameContext
        {
            Player = player,
            Board = _board,
            TurnNumber = 1
        };

        #region SkipTurns

        /// <summary>
        /// Verifies that TurnSkipped is true when SkipTurns is greater than zero.
        /// </summary>
        [Fact]
        public void Apply_WithSkipTurns_SetsTurnSkippedTrue()
        {
            _players[0].Piece.SkipTurns = 2;

            _rule.Apply(CreateContext(_players[0]));

            Assert.True(_rule.TurnSkipped);
        }

        /// <summary>
        /// Verifies that SkipTurns decrements by one when applied.
        /// </summary>
        [Fact]
        public void Apply_WithSkipTurns_DecrementsSkipTurns()
        {
            _players[0].Piece.SkipTurns = 2;

            _rule.Apply(CreateContext(_players[0]));

            Assert.Equal(1, _players[0].Piece.SkipTurns);
        }

        /// <summary>
        /// Verifies that TurnSkipped is false when SkipTurns reaches zero.
        /// </summary>
        [Fact]
        public void Apply_WithSkipTurnsAtZero_SetsTurnSkippedFalse()
        {
            _players[0].Piece.SkipTurns = 0;

            _rule.Apply(CreateContext(_players[0]));

            Assert.False(_rule.TurnSkipped);
        }

        #endregion

        #region IsStuck

        /// <summary>
        /// Verifies that TurnSkipped is true when piece is stuck.
        /// </summary>
        [Fact]
        public void Apply_WhenIsStuck_SetsTurnSkippedTrue()
        {
            _players[0].Piece.IsStuck = true;

            _rule.Apply(CreateContext(_players[0]));

            Assert.True(_rule.TurnSkipped);
        }

        /// <summary>
        /// Verifies that TurnSkipped is false when piece is not stuck and has no skip turns.
        /// </summary>
        [Fact]
        public void Apply_WhenNotStuckAndNoSkipTurns_SetsTurnSkippedFalse()
        {
            _players[0].Piece.IsStuck = false;
            _players[0].Piece.SkipTurns = 0;

            _rule.Apply(CreateContext(_players[0]));

            Assert.False(_rule.TurnSkipped);
        }

        /// <summary>
        /// Verifies that IsStuck does not decrement anything.
        /// </summary>
        [Fact]
        public void Apply_WhenIsStuck_DoesNotDecrementSkipTurns()
        {
            _players[0].Piece.IsStuck = true;
            _players[0].Piece.SkipTurns = 0;

            _rule.Apply(CreateContext(_players[0]));

            Assert.Equal(0, _players[0].Piece.SkipTurns);
        }

        #endregion
    }
}