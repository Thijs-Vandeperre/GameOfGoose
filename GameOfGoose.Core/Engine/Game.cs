using System.Collections.Generic;
using System.Linq;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Logging;

namespace GameOfGoose.Core.Engine
{
    /// <summary>
    /// Represents and controls the Game of Goose game.
    /// </summary>
    public class Game
    {
        private int _turnNumber = 1;
        private GameStatus _gameStatus = GameStatus.InProgress;
        private readonly IDiceRoll _diceRoll;
        private readonly ILogger _logger;
        private readonly IGameFormatter _formatter;
        private readonly IInputReader _inputReader;
        private readonly IEnumerable<IGameRule> _rules;

        /// <summary>
        /// Gets the list of players in the game.
        /// </summary>
        public IReadOnlyList<Player> Players { get; }

        /// <summary>
        /// Gets the game board.
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Game"/>.
        /// </summary>
        public Game(IReadOnlyList<Player> players, Board board, IDiceRoll diceRoll, ILogger logger, IGameFormatter formatter, IInputReader inputReader, IEnumerable<IGameRule> rules)
        {
            Players = players;
            Board = board;
            _diceRoll = diceRoll;
            _logger = logger;
            _formatter = formatter;
            _inputReader = inputReader;
            _rules = rules;
        }

        /// <summary>
        /// Starts the game and runs until a player wins.
        /// </summary>
        public void Start()
        {
            _logger.Log(_formatter.FormatHeader(Players.Count));
            while (_gameStatus == GameStatus.InProgress)
            {
                NextRound();
            }
        }

        /// <summary>
        /// Processes one round for all players and increments the turn number.
        /// </summary>
        private void NextRound()
        {
            _logger.Log(_formatter.FormatTurnStart(_turnNumber));
            var turnResults = new List<string>();
            foreach (var player in Players)
            {
                turnResults.Add(NextTurn(player));
                if (player.Piece.HasWon)
                {
                    _logger.Log(_formatter.FormatTurnResults(turnResults));
                    End(player);
                    return;
                }
            }
            _logger.Log(_formatter.FormatTurnResults(turnResults));
            _turnNumber++;
            _logger.Log(_formatter.FormatPressEnter(_turnNumber));
            _inputReader.WaitForEnter();
        }

        /// <summary>
        /// Processes one turn for the specified player.
        /// </summary>
        private string NextTurn(Player player)
        {
            if (player.Piece.SkipTurns > 0 || player.Piece.IsStuck)
            {
                if (player.Piece.SkipTurns > 0)
                    player.Piece.SkipTurns--;

                return _formatter.FormatSkipTurn(player.Piece.CurrentPosition);
            }

            player.Piece.IsMovingForward = true;

            var (roll1, roll2) = _diceRoll.DoubleRoll();

            player.Piece.MoveTo(player.Piece.CurrentPosition + roll1 + roll2);

            var context = new GameContext
            {
                Player = player,
                Board = Board,
                Roll1 = roll1,
                Roll2 = roll2,
                TurnNumber = _turnNumber
            };

            foreach (var rule in _rules.OrderBy(r => r.Order).Where(r => !(r is SpaceActionRule)))
            {
                rule.Apply(context);
            }

            int positionAfterBounce = player.Piece.CurrentPosition;

            foreach (var rule in _rules.OfType<SpaceActionRule>())
            {
                rule.Apply(context);
            }

            int finalPosition = player.Piece.CurrentPosition;

            return _formatter.FormatTurnResult(roll1, roll2, positionAfterBounce, finalPosition);
        }

        /// <summary>
        /// Ends the game and logs the winner.
        /// </summary>
        private void End(Player player)
        {
            _gameStatus = GameStatus.Finished;
            _logger.Log("WINNER!!!");
            _logger.Log(_formatter.FormatWinner(player.Name));
            _logger.Log(_formatter.FormatPressEnterFinish());
            _inputReader.WaitForEnter();
        }
    }
}