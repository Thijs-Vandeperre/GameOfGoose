namespace GameOfGoose.Core.Engine.Rules
{
    /// <summary>
    /// Handles bounce-back when a piece overshoots the final position.
    /// </summary>
    public class BounceRule : IGameRule
    {
        public int Order => 10;

        /// <summary>
        /// Applies the bounce logic to a piece given the end position.
        /// </summary>
        /// <param name="piece">The piece to apply the bounce to.</param>
        /// <param name="endPosition">The end position of the board.</param>
        public static void ApplyBounce(Piece piece, int endPosition)
        {
            if (piece.CurrentPosition <= endPosition)
                return;

            int bounce = piece.CurrentPosition - endPosition;
            piece.IsMovingForward = false;
            piece.MoveTo(endPosition - bounce);
        }

        /// <summary>
        /// Applies the rule to the current game context.
        /// </summary>
        /// <param name="context">The game context for the current turn.</param>
        public void Apply(GameContext context)
        {
            ApplyBounce(context.Player.Piece, context.Board.EndPosition);
        }
    }
}