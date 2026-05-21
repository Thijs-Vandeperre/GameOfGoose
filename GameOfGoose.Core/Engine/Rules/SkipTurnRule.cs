namespace GameOfGoose.Core.Engine.Rules
{
    /// <summary>
    /// Handles skipping a turn for pieces that are stuck or have pending skip turns.
    /// </summary>
    public class SkipTurnRule : IGameRule
    {
        public int Order => 0;

        /// <summary>
        /// Gets whether the rule caused the turn to be skipped.
        /// </summary>
        public bool TurnSkipped { get; private set; }

        /// <summary>
        /// Applies the rule to the current game context.
        /// </summary>
        /// <param name="context">The game context for the current turn.</param>
        public void Apply(GameContext context)
        {
            var piece = context.Player.Piece;

            if (piece.SkipTurns > 0)
            {
                piece.SkipTurns--;
                TurnSkipped = true;
                return;
            }

            if (piece.IsStuck)
            {
                TurnSkipped = true;
                return;
            }

            TurnSkipped = false;
        }
    }
}