namespace GameOfGoose.Core.Engine.Rules
{
    /// <summary>
    /// Represents a game rule that can modify game state during a turn.
    /// </summary>
    public interface IGameRule
    {
        /// <summary>
        /// Gets the execution order of this rule. Rules are applied in ascending order.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Applies the rule to the current game context.
        /// </summary>
        /// <param name="context">The game context for the current turn.</param>
        void Apply(GameContext context);
    }
}