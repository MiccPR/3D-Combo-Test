public class PlayerStateMachine : StateMachine
{
    // highest level state machine
    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

    // contains all sub-state machines
    public PlayerMovementStateMachine MovementStateMachine { get; private set; }
    public PlayerCombatStateMachine CombatStateMachine { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        InitializeStateMachines();
    }

    private void InitializeStateMachines()
    {
        MovementStateMachine = new PlayerMovementStateMachine(Player, this);
        CombatStateMachine = new PlayerCombatStateMachine(Player, this);
    }
}
