using System.Collections.Generic;
using GameOfGoose.Core;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Factories;
using GameOfGoose.Core.Engine;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Logging;

namespace GameOfGoose.Core
{
    /// <summary>
    /// Entry point for the Game of Goose application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        private static void Main(string[] args)
        {
            var pieces = PieceFactory.CreatePieces(4);
            var players = PlayerFactory.CreatePlayers(pieces);
            var board = BoardFactory.CreateBoard(pieces);
            var diceRoll = new TwoDiceRoll(new Die());
            var logger = new ConsoleLogger();
            var inputReader = new ConsoleInputReader();
            var rules = new List<IGameRule>
            {
                new FirstTurnRule(),
                new BounceRule(),
                new SpaceActionRule()
            };

            var game = new Game(players, board, diceRoll, logger, inputReader, rules);
            game.Start();
        }
    }
}