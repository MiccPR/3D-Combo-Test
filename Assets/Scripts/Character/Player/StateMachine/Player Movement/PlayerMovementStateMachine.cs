public class PlayerMovementStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }
    public PlayerIdleState IdleState { get; }
    public PlayerRunningState RunningState { get; }

    public PlayerMovementStateMachine(Player player, PlayerStateMachine playerStateMachine)
    {
        Player = player;
        ReusableData = playerStateMachine.ReusableData;

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
    }
}
