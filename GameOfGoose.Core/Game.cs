using System.Collections.Generic;
using GameOfGoose.Core.Dice;
using GameOfGoose.Core.Logging;
using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Core
{
    /// <summary>
    /// Represents and controls the Game of Goose game.
    /// </summary>
    public class Game
    {
        private int _turnNumber = 1;
        private GameStatus _gameStatus = GameStatus.InProgress;
        private readonly ILogger _logger;
        private readonly IInputReader _inputReader;

        /// <summary>
        /// Gets the list of players in the game.
        /// </summary>
        public IReadOnlyList<Player> Players { get; }

        /// <summary>
        /// Gets the game board.
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// Gets the dice roll used to generate random rolls.
        /// </summary>
        public TwoDiceRoll DiceRoll { get; }
        
        /// <summary>
        /// Initializes a new instance of <see cref="Game"/> with the specified players, board, dice roll, logger and input reader.
        /// </summary>
        /// <param name="players">The list of players participating in the game.</param>
        /// <param name="board">The game board containing all spaces.</param>
        /// <param name="diceRoll">The dice roll used to generate random roll values.</param>
        /// <param name="logger">The logger used to output game messages.</param>
        /// <param name="inputReader">The input reader used to wait for player input.</param>
        public Game(List<Player> players, Board board, TwoDiceRoll diceRoll, ILogger logger, IInputReader inputReader)
        {
            Players = players;
            Board = board;
            DiceRoll = diceRoll;
            _logger = logger;
            _inputReader = inputReader;
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
                    End(player);
                    break;
                }
            }
            _logger.Log(string.Join("\t", turnResults));
            if (_gameStatus == GameStatus.InProgress)
            {
                _turnNumber++;
                _logger.Log($"[Press ENTER to play Turn {_turnNumber}]");
                _inputReader.WaitForEnter();
            }
        }

        /// <summary>
        /// Processes one turn for the specified player.
        /// </summary>
        /// <param name="player">The player whose turn it is.</param>
        /// <returns>A formatted string representing the result of the player's turn.</returns>
        internal string NextTurn(Player player)
        {
            if (player.Piece.SkipTurns > 0)
            {
                player.Piece.SkipTurns--;
                return $"/ :{FormatPosition(player.Piece.CurrentPosition)}";
            }

            player.Piece.IsMovingForward = true;
            var (roll1, roll2) = DiceRoll.DoubleRoll();
            int roll = roll1 + roll2;

            if (_turnNumber != 1 || !HandleFirstTurn(player.Piece, roll1, roll2))
            {
                int newPosition = player.Piece.CurrentPosition + roll;
                if (newPosition > Board.EndPosition)
                {
                    newPosition = HandleBounce(player.Piece, newPosition);
                }
                player.Piece.MoveTo(newPosition);
            }

            int positionBeforeSpaceAction = player.Piece.CurrentPosition;
            Board.GetSpace(player.Piece.CurrentPosition).SpaceAction(player.Piece, roll);
            HandleGooseChaining(player.Piece, roll);

            if (player.Piece.CurrentPosition != positionBeforeSpaceAction)
            {
                return $"{roll1}+{roll2}: {FormatPosition(positionBeforeSpaceAction)}->{FormatPosition(player.Piece.CurrentPosition)}";
            }
            else
            {
                return $"{roll1}+{roll2}: {FormatPosition(player.Piece.CurrentPosition)}";
            }
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
        /// Handles the special first turn rules for 5+4 and 6+3 rolls.
        /// </summary>
        /// <param name="piece">The piece to move.</param>
        /// <param name="roll1">The value of the first die.</param>
        /// <param name="roll2">The value of the second die.</param>
        /// <returns>True if a special case was handled, false otherwise.</returns>
        internal bool HandleFirstTurn(Piece piece, int roll1, int roll2)
        {
            if ((roll1 == 5 && roll2 == 4) || (roll1 == 4 && roll2 == 5))
            {
                piece.MoveTo(26);
                return true;
            }
            else if ((roll1 == 6 && roll2 == 3) || (roll1 == 3 && roll2 == 6))
            {
                piece.MoveTo(53);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates the bounce-back position when a piece overshoots the end.
        /// </summary>
        /// <param name="piece">The piece that overshot the end.</param>
        /// <param name="newPosition">The calculated position before bouncing.</param>
        /// <returns>The final position after bouncing back.</returns>
        internal int HandleBounce(Piece piece, int newPosition)
        {
            var bounce = newPosition - Board.EndPosition;
            piece.IsMovingForward = false;
            return Board.EndPosition - bounce;
        }

        /// <summary>
        /// Handles goose chaining by repeatedly applying the goose space action while the piece is on a goose space.
        /// </summary>
        /// <param name="piece">The piece to apply goose chaining to.</param>
        /// <param name="roll">The total dice roll value used for movement</param>
        private void HandleGooseChaining(Piece piece, int roll)
        {
            while (Board.GetSpace(piece.CurrentPosition) is Goose)
            {
                Board.GetSpace(piece.CurrentPosition).SpaceAction(piece, roll);
            }
        }

        /// <summary>
        /// Formats a board position as a string, returning "Start" for position 0.
        /// </summary>
        /// <param name="position">The board position to format</param>
        /// <returns>A formatted position string.</returns>
        internal string FormatPosition(int position) => position == 0 ? "Start" : $"S{position}";
    }
}