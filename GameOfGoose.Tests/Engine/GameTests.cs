using GameOfGoose.Core;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Factories;
using GameOfGoose.Core.UI;
using System.Collections.Generic;

namespace GameOfGoose.Tests.Engine
{
    /// <summary>
    /// Contains unit tests for the Game class.
    /// </summary>
    public class GameTests
    {
        private class NoOpInputReader : IInputReader
        {
            public void WaitForEnter() { }
        }

        private class NoOpLogger : ILogger
        {
            public void Log(string message) { }
        }

        private Game CreateGame(IReadOnlyList<Player> players, Board board, IDiceRoll diceRoll)
        {
            var rules = new List<IGameRule>
            {
                new FirstTurnRule(),
                new BounceRule(),
                new SpaceActionRule()
            };

            return new Game(players, board, diceRoll, new NoOpLogger(), new GameFormatter(), new NoOpInputReader(), rules);
        }

        #region SkipTurns

        /// <summary>
        /// Verifies that a piece with skip turns does not move during Start.
        /// </summary>
        [Fact]
        public void Start_WithSkipTurns_DoesNotMovePiece()
        {
            var pieces = PieceFactory.CreatePieces(1);
            pieces[0].MoveTo(61);
            pieces[0].SkipTurns = 1;
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);

            // turn 1: skipped (SkipTurns goes to 0)
            // turn 2: rolls 1+1 = 2, lands on 63 (End)
            var game = CreateGame(players, board, new TwoDiceRoll(new FakeDie(1, 1, 1, 1)));

            game.Start();

            Assert.True(pieces[0].HasWon);
            Assert.Equal(63, pieces[0].CurrentPosition);
        }

        /// <summary>
        /// Verifies that skip turns decrement correctly during Start.
        /// </summary>
        [Fact]
        public void Start_WithSkipTurns_DecrementsSkipTurns()
        {
            var pieces = PieceFactory.CreatePieces(1);
            pieces[0].MoveTo(61);
            pieces[0].SkipTurns = 1;
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);

            // turn 1: skipped, turn 2: wins
            var game = CreateGame(players, board, new TwoDiceRoll(new FakeDie(1, 1, 1, 1)));

            game.Start();

            Assert.Equal(0, pieces[0].SkipTurns);
        }

        #endregion

        #region Movement

        /// <summary>
        /// Verifies that Start terminates when a player wins.
        /// </summary>
        [Fact]
        public void Start_TerminatesWhenPlayerWins()
        {
            var pieces = PieceFactory.CreatePieces(1);
            pieces[0].MoveTo(61);
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);

            // rolls 1+1 = 2, lands on 63 (End)
            var game = CreateGame(players, board, new TwoDiceRoll(new FakeDie(1, 1)));

            game.Start();

            Assert.True(pieces[0].HasWon);
        }

        /// <summary>
        /// Verifies that only the winning player has HasWon set to true.
        /// </summary>
        [Fact]
        public void Start_SetsHasWonOnlyOnWinningPiece()
        {
            var pieces = PieceFactory.CreatePieces(2);
            pieces[0].MoveTo(61);
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);

            // player 1 rolls 1+1 = 2, lands on 63 (End), player 2 never gets to move
            var game = CreateGame(players, board, new TwoDiceRoll(new FakeDie(1, 1)));

            game.Start();

            Assert.True(pieces[0].HasWon);
            Assert.False(pieces[1].HasWon);
        }

        #endregion
    }
}