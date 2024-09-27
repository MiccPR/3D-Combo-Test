public class PlayerCombatStateMachine : StateMachine
{
    // combat sub-state
    public PlayerStateMachine PlayerStateMachine { get; }

    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

    public PlayerNormalAttackState NormalAttackState { get; }

    public PlayerCombatStateMachine(Player player, PlayerStateMachine playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;

        Player = player;
        ReusableData = playerStateMachine.ReusableData;

        NormalAttackState = new PlayerNormalAttackState(this);
    }
}
