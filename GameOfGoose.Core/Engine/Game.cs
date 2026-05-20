using System.Collections.Generic;
using System.Linq;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Engine.Rules;
using GameOfGoose.Core.Logging;
using GameOfGoose.Core.Spaces;

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
        /// Initializes a new instance of <see cref="Game"/> with the specified players, board, dice roll, logger and input reader.
        /// </summary>
        /// <param name="players">The list of players participating in the game.</param>
        /// <param name="board">The game board containing all spaces.</param>
        /// <param name="diceRoll">The dice roll used to generate random roll values.</param>
        /// <param name="logger">The logger used to output game messages.</param>
        /// <param name="inputReader">The input reader used to wait for player input.</param>
        public Game(IReadOnlyList<Player> players, Board board, IDiceRoll diceRoll, ILogger logger, IInputReader inputReader, IEnumerable<IGameRule> rules)
        {
            Players = players;
            Board = board;
            _diceRoll = diceRoll;
            _logger = logger;
            _inputReader = inputReader;
            _rules = rules;
        }

        /// <summary>
        /// Starts the game and runs until a player wins.
        /// </summary>
        public void Start()
        {
            _logger.Log("\tPIECE 1\tPIECE 2\tPIECE 3\tPIECE 4");
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
            _logger.Log($"Turn {_turnNumber}");
            var turnResults = new List<string>();
            foreach (var player in Players)
            {
                turnResults.Add(NextTurn(player));
                if (player.Piece.HasWon)
                {
                    _logger.Log(string.Join("\t", turnResults));
                    End(player);
                    return;
                }
            }
            _logger.Log(string.Join("\t", turnResults));
            _turnNumber++;
            _logger.Log($"[Press ENTER to play Turn {_turnNumber}]");
            _inputReader.WaitForEnter();
        }

        /// <summary>
        /// Processes one turn for the specified player.
        /// </summary>
        /// <param name="player">The player whose turn it is.</param>
        /// <returns>A formatted string representing the result of the player's turn.</returns>
        internal string NextTurn(Player player)
        {
            if (player.Piece.SkipTurns > 0 || player.Piece.IsStuck)
            {
                if (player.Piece.SkipTurns > 0)
                {
                    player.Piece.SkipTurns--;
                }
                return $"/ :{FormatPosition(player.Piece.CurrentPosition)}";
            }

            player.Piece.IsMovingForward = true;

            var (roll1, roll2) = _diceRoll.DoubleRoll();
            int roll = roll1 + roll2;

            player.Piece.MoveTo(player.Piece.CurrentPosition + roll);

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

            if (finalPosition != positionAfterBounce)
            {
                return $"{roll1}+{roll2}: {FormatPosition(positionAfterBounce)}->{FormatPosition(finalPosition)}"; 
            }
            
            return $"{roll1}+{roll2}: {FormatPosition(finalPosition)}";
        }

        /// <summary>
        /// Ends the game and logs the winner.
        /// </summary>
        /// <param name="player">The player who won the game.</param>
        private void End(Player player)
        {
            _gameStatus = GameStatus.Finished;
            _logger.Log("WINNER!!!");
            _logger.Log($"{player.Name} has won!");
            _logger.Log("Press ENTER to FINISH GAME");
            _inputReader.WaitForEnter();
        }

        /// <summary>
        /// Formats a board position as a string, returning "Start" for position 0.
        /// </summary>
        /// <param name="position">The board position to format</param>
        /// <returns>A formatted position string.</returns>
        internal string FormatPosition(int position) => position == 0 ? "Start" : $"S{position}";
    }
}