public class PlayerCombatStateMachine : StateMachine
{
    public PlayerStateMachine PlayerStateMachine { get; }

    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

    public PlayerMovementStateMachine PlayerMovementStateMachine { get; }

    public PlayerNormalAttackState NormalAttackState { get; }

    public PlayerCombatStateMachine(Player player, PlayerStateMachine playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;

        Player = player;
        ReusableData = playerStateMachine.ReusableData;

        PlayerMovementStateMachine = playerStateMachine.MovementStateMachine;

        NormalAttackState = new PlayerNormalAttackState(this);
    }
}
