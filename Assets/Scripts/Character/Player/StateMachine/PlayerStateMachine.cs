public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

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
