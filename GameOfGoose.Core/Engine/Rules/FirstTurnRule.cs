namespace GameOfGoose.Core.Engine.Rules
{
    /// <summary>
    /// Handles special first turn rules (5+4 and 6+3 jumps).
    /// </summary>
    public class FirstTurnRule : IGameRule
    {
        public int Order => 30;

        /// <summary>
        /// Applies the rule to the current game context.
        /// </summary>
        /// <param name="context">The game context for the current turn.</param>
        public void Apply(GameContext context)
        {
            if (context.TurnNumber != 1)
                return;

            var piece = context.Player.Piece;

            if ((context.Roll1 == 5 && context.Roll2 == 4) ||
                (context.Roll1 == 4 && context.Roll2 == 5))
            {
                piece.MoveTo(26);
            }
            else if ((context.Roll1 == 6 && context.Roll2 == 3) ||
                     (context.Roll1 == 3 && context.Roll2 == 6))
            {
                piece.MoveTo(53);
            }
        }
    }
}