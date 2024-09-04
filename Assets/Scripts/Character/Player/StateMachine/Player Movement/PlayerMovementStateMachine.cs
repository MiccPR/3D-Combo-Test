public class PlayerMovementStateMachine : StateMachine
{
    public PlayerStateMachine PlayerStateMachine { get; }

    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

    public PlayerCombatStateMachine CombatStateMachine { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerRunningState RunningState { get; }

    public PlayerMovementStateMachine(Player player, PlayerStateMachine playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;

        Player = player;
        ReusableData = playerStateMachine.ReusableData;

        CombatStateMachine = playerStateMachine.CombatStateMachine;

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
    }
}
