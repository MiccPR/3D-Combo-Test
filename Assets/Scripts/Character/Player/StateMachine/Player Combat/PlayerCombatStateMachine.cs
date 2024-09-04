public class PlayerCombatStateMachine : PlayerStateMachine
{
    public PlayerNormalAttackState NormalAttackState { get; }

    public PlayerCombatStateMachine(Player player) : base(player)
    {
        // NormalAttackState = new PlayerNormalAttackState(this);
    }
}
