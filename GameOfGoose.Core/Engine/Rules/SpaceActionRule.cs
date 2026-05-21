using GameOfGoose.Core.Spaces;

namespace GameOfGoose.Core.Engine.Rules
{
    /// <summary>
    /// Applies the action of the space a piece has landed on, including goose chaining.
    /// </summary>
    public class SpaceActionRule : IGameRule
    {
        public int Order => 20;

        /// <summary>
        /// Applies the rule to the current game context.
        /// </summary>
        /// <param name="context">The game context for the current turn.</param>
        public void Apply(GameContext context)
        {
            var piece = context.Player.Piece;
            var space = context.Board.GetSpace(piece.CurrentPosition);

            if (space == null)
                return;

            space.SpaceAction(piece, context.TotalRoll);

            while (true)
            {
                BounceRule.ApplyBounce(piece, context.Board.EndPosition);

                var currentSpace = context.Board.GetSpace(piece.CurrentPosition);
                if (currentSpace is Goose goose)
                {
                    goose.SpaceAction(piece, context.TotalRoll);
                }
                else
                {
                    currentSpace.SpaceAction(piece, context.TotalRoll);
                    break;
                }
            }
        }
    }
}