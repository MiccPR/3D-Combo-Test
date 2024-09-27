public class PlayerMovementStateMachine : StateMachine
{
    // movement sub-state
    public PlayerStateMachine PlayerStateMachine { get; }

    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

    public PlayerIdlingState IdlingState { get; }
    public PlayerRunningState RunningState { get; }

    public PlayerMediumStoppingState MediumStoppingState { get; }

    public PlayerMovementStateMachine(Player player, PlayerStateMachine playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;

        Player = player;
        ReusableData = playerStateMachine.ReusableData;

        IdlingState = new PlayerIdlingState(this);
        RunningState = new PlayerRunningState(this);
        MediumStoppingState = new PlayerMediumStoppingState(this);
    }
}
