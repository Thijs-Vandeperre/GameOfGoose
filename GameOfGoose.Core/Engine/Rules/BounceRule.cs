namespace GameOfGoose.Core.Engine.Rules
{
    /// <summary>
    /// Handles bounce-back when a piece overshoots the final position.
    /// </summary>
    public class BounceRule : IGameRule
    {
        public int Order => 10;

        /// <summary>
        /// Applies the rule to the current game context.
        /// </summary>
        /// <param name="context">The game context for the current turn.</param>
        public void Apply(GameContext context)
        {
            var piece = context.Player.Piece;
            int end = context.Board.EndPosition;

            if (piece.CurrentPosition <= end)
                return;

            int bounce = piece.CurrentPosition - end;

            piece.IsMovingForward = false;
            piece.MoveTo(end - bounce);
        }
    }
}